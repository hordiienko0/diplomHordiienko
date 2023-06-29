using Ctor.Application.Common.Events;

namespace Ctor.Application.MyEntity.Commands;

public class BusTestPushEvent : Event
{
    public BusTestPushEvent(string message, double price)
    {
        Message = message;
        Price = price;
    }

    public string Message { get; private set; }
    public double Price { get; private set; }

}