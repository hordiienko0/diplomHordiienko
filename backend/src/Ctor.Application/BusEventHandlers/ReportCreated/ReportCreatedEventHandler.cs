using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Interfaces.Bus;
using Ctor.Application.DTOs;
using Ctor.Domain.Entities.Enums;
using Microsoft.Extensions.Logging;

namespace Ctor.Application.BusEventHandlers.ReportCreated;

public class ReportCreatedEventHandler : IEventHandler<ReportCreatedEvent>
{
    private readonly ILogger<ReportCreatedEventHandler> _logger;
    private readonly INotificationService _notificationService;

    public ReportCreatedEventHandler(ILogger<ReportCreatedEventHandler> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }
    public Task Handle(ReportCreatedEvent @event)
    {
        _notificationService.SendNotificationToUser(
            new NotificationDTO() { Message = "Report for project was created", type = NotificationTypes.Success },
            @event.UserId);
        _logger.LogInformation($"Report for project by id {@event.ProjectId} created");
        return Task.FromResult(true);
    }
}