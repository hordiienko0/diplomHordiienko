namespace Ctor.Application.Common.Events;

public class CreateReportEvent : Event
{
    public CreateReportEvent(long projectId, long userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    public long ProjectId { get; }
    public long UserId { get; }
}