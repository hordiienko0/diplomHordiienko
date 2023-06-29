using Ctor.Application.Common.Events;
using Ctor.Application.MyEntity.Commands;

namespace Ctor.Application.BusEventHandlers.BusTestReceive;

public class BusTestReceiveEvent : Event
{
    public BusTestReceiveEvent(string message)
    {
        Message = message;
    }

    public string Message { get; private set; }
}