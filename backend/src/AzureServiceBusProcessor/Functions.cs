using System;
using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusProcessor;

public static class Functions
{

    [FunctionName("Function1")]
    public static void Run([ServiceBusTrigger("bustestreceiveevent", "BusTestReceiveEventHandler", Connection = "AzureServiceBusConnection")] ServiceBusReceivedMessage message, ILogger logger)
    {
        logger.LogInformation($"C# ServiceBus topic trigger function processed message: {message.Body}");
    }
}
