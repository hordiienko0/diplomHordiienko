using System.Text;
using Ctor.Application.Common.Events;
using Ctor.Application.Common.Interfaces.Bus;
using Ctor.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ctor.Infrastructure.Services;

public class RabbitMqBus : IEventBus
{
    private readonly ILogger<RabbitMqBus> _logger;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly MessageBrokerSettings _messageBrokerSettings;

    public RabbitMqBus(IServiceScopeFactory serviceScopeFactory, MessageBrokerSettings brokerSettings,
        ILogger<RabbitMqBus> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
        _logger = logger;
        _messageBrokerSettings = brokerSettings;
    }

    public Task Publish<T>(T @event, CancellationToken cancellationToken = default) where T : Event
    {
        ConnectionFactory factory = new()
        {
            HostName = _messageBrokerSettings.HostName,
            UserName = _messageBrokerSettings.UserName,
            Password = _messageBrokerSettings.Password,
            Port = _messageBrokerSettings.Port
        };
        using IConnection? connection = factory.CreateConnection();
        using IModel? channel = connection.CreateModel();
        var eventName = @event.GetType().Name;
        channel.QueueDeclare(eventName, false, false, false, null);
        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("", eventName, null, body);

        return Task.CompletedTask;
    }

    public Task Subscribe<T, TH>(CancellationToken cancellationToken = default) where T : Event where TH : IEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        if (_handlers[eventName].Any(s => s == handlerType))
        {
            _logger.LogError($"Handler Type {handlerType.Name} already is registered for '{eventName}'",
                nameof(handlerType));
            throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'",
                nameof(handlerType));
        }

        _handlers[eventName].Add(handlerType);

        StartBasicConsumer<T>();

        return Task.CompletedTask;
    }

    private void StartBasicConsumer<T>() where T : Event
    {
        ConnectionFactory factory = new()
        {
            HostName = _messageBrokerSettings.HostName,
            UserName = _messageBrokerSettings.UserName,
            Password = _messageBrokerSettings.Password,
            Port = _messageBrokerSettings.Port,
            DispatchConsumersAsync = true
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var eventName = typeof(T).Name;
        channel.QueueDeclare(eventName, false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;

        channel.BasicConsume(eventName, false, consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.Span);

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
            ((AsyncDefaultBasicConsumer)sender).Model.BasicAck(e.DeliveryTag, false);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong with Consumer_Received!");
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_handlers.ContainsKey(eventName))
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);

                    if (handler == null)
                    {
                        continue;
                    }

                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}