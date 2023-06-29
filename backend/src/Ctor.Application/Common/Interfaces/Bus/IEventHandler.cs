namespace Ctor.Application.Common.Interfaces.Bus;

public interface IEventHandler<in TEvent> : IEventHandler
{
    Task Handle(TEvent @event);
}

public interface IEventHandler
{

}