namespace Ctor.Application.Common.Events;

public abstract class Event
{
    public DateTime TimeStamps { get; protected set; }
    public string Name { get; protected set; }
    protected Event()
    {
        TimeStamps = DateTime.Now;
        Name = GetType().Name;
    }
}