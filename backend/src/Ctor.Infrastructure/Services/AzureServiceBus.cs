using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Ctor.Application.Common.Events;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces.Bus;
using Ctor.Application.MyEntity.Commands;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Ctor.Infrastructure.Services;

public class AzureServiceBus : IEventBus
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusAdministrationClient _serviceBusAdminClient;
    private readonly ManagementClient _managementClient;

    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;


    private readonly ILogger<AzureServiceBus> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AzureServiceBus(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, ILogger<AzureServiceBus> logger)
    {
        var connectionString = configuration.GetConnectionString("AzureServiceBusConnection");
        _serviceBusClient = new ServiceBusClient(connectionString);
        _serviceBusAdminClient = new ServiceBusAdministrationClient(connectionString);
        _managementClient = new ManagementClient(connectionString);
        _logger = logger;
        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Publish<T>(T @event, CancellationToken cancellationToken = default)
        where T : Event
    {
        var topicName = typeof(T).Name;

        if (!await _managementClient.TopicExistsAsync(topicName, cancellationToken))
            await _managementClient.CreateTopicAsync(topicName, cancellationToken);

        var sender = _serviceBusClient.CreateSender(topicName);
        var message = new ServiceBusMessage(JsonConvert.SerializeObject(@event));

        await sender.SendMessageAsync(message, cancellationToken);
        await sender.DisposeAsync();
    }   
    

    public async Task Subscribe<T, TH>(CancellationToken cancellationToken = default)
        where T : Event
        where TH : IEventHandler<T>
    {
        var eventType = typeof(T);
        var handlerType = typeof(TH);
        

        if (!_eventTypes.Contains(eventType))
            _eventTypes.Add(eventType);

        if (!_handlers.ContainsKey(eventType.Name))
            _handlers.Add(eventType.Name, new List<Type>());

        
        if (_handlers[eventType.Name].Any(s => s == typeof(TH)))
        {
            _logger.LogError($"Handler Type {handlerType.Name} already is registered for '{eventType.Name}'", nameof(handlerType));
            throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventType.Name}'", nameof(handlerType));
        }

        _handlers[eventType.Name].Add(handlerType);

        if (!await _managementClient.TopicExistsAsync(eventType.Name, cancellationToken))
            await _managementClient.CreateTopicAsync(handlerType.Name, cancellationToken);

        if (!await _managementClient.SubscriptionExistsAsync(eventType.Name, handlerType.Name, cancellationToken))
            await _managementClient.CreateSubscriptionAsync( new SubscriptionDescription(eventType.Name, handlerType.Name), cancellationToken);


        await CreateProcessor<T, TH>();

    }

    private async Task CreateProcessor<T, TH>()
        where T : Event
        where TH : IEventHandler<T>
    {
        var topicName = typeof(T).Name;
        var subscriptionName = typeof(TH).Name;

        var serviceBusProcessorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 3,
            AutoCompleteMessages = false,
        };

        try
        { 
            var processor = _serviceBusClient.CreateProcessor(topicName, subscriptionName, serviceBusProcessorOptions);

            processor.ProcessMessageAsync += ProcessMessagesAsync;
            processor.ProcessErrorAsync += ProcessErrorAsync;

            await processor.StartProcessingAsync().ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Something went wrong with processor!");
        }
    }

    private async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
    {
        var messageBody = Encoding.UTF8.GetString(args.Message.Body);
        await ProcessEvent(messageBody).ConfigureAwait(false);
    }

    private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
    {
        _logger.LogError(arg.Exception, "Message handler encountered an exception");
        _logger.LogDebug($"- ErrorSource: {arg.ErrorSource}");
        _logger.LogDebug($"- Entity Path: {arg.EntityPath}");
        _logger.LogDebug($"- FullyQualifiedNamespace: {arg.FullyQualifiedNamespace}");

        return Task.CompletedTask;
    }

    private async Task ProcessEvent(string message)
    {
        var @dynamicEvent = JsonConvert.DeserializeObject<dynamic>(message);
        string eventName = @dynamicEvent.Name.ToString();

        if (_handlers.ContainsKey(eventName))
        {

            var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
            var @event = JsonConvert.DeserializeObject(message, eventType);
            var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);

                    if (handler == null)
                        continue;

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
