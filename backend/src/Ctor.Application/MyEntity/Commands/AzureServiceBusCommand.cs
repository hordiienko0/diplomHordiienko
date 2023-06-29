using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.BusEventHandlers.BusTestReceive;
using Ctor.Application.Common.Events;
using Ctor.Application.Common.Interfaces.Bus;
using MediatR;

namespace Ctor.Application.MyEntity.Commands;

public record AzureServiceBusCommand() : IRequest;
public class AzureServiceBusCommandHandler : IRequestHandler<AzureServiceBusCommand, Unit>
{
    private readonly IEventBus _serviceBus;
    public AzureServiceBusCommandHandler(IEventBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task<Unit> Handle(AzureServiceBusCommand request,
       CancellationToken cancellationToken)
    {
        _serviceBus.Publish(new BusTestReceiveEvent("Hello"), cancellationToken);
        return Unit.Value;
    }

}