using Ctor.Application.Common.Events;

namespace Ctor.Application.Common.Interfaces.Bus;

public interface IEventBus
{
    Task Publish<T>(T @event, CancellationToken cancellationToken = default) where T : Event;
    Task Subscribe<T, TH>(CancellationToken cancellationToken = default)
        where T : Event
        where TH : IEventHandler<T>;
}