using Ctor.Application.Common.Interfaces.Bus;
using Ctor.Application.MyEntity.Commands;
using Microsoft.Extensions.Logging;

namespace Ctor.Application.BusEventHandlers.BusTestReceive;

public class BusTestReceiveEventHandler : IEventHandler<BusTestReceiveEvent>
{
    private readonly ILogger<BusTestReceiveEventHandler> _logger;

    public BusTestReceiveEventHandler(ILogger<BusTestReceiveEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(BusTestReceiveEvent receiveEvent)
    {
        _logger.LogInformation($"Message: {receiveEvent.Message};");
        return Task.FromResult(true);
    }
}