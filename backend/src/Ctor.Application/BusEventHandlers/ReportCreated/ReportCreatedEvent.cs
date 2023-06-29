using Ctor.Application.Common.Events;

namespace Ctor.Application.BusEventHandlers.ReportCreated;

public class ReportCreatedEvent : Event
{
    public ReportCreatedEvent(long projectId, long userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    public long UserId { get; set; }

    public long ProjectId { get; set; }
}