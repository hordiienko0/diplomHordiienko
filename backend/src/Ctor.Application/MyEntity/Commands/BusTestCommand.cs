using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Interfaces.Bus;
using Ctor.Application.DTOs;
using MediatR;

namespace Ctor.Application.MyEntity.Commands;

public record BusTestCommand() : IRequest;
public class RabbitMqTestCommandHandler : IRequestHandler<BusTestCommand, Unit>
{
    private readonly IEventBus _eventBus;

    public RabbitMqTestCommandHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(BusTestCommand request,
        CancellationToken cancellationToken)
    {
        _eventBus.Publish(new BusTestPushEvent("Hello", 342.2));
        return Unit.Value;
    }


}