using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs;
using Ctor.Application.Notifications.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Ctor.Services;

/*
 *  Use this service for sending notification.
 *  Inside request handler, using di, add INotificationService,
 *  and send notifications to user or group.
 *  ass parameter in function put NotificationDTO.
 */

public class NotificationService : INotificationService
{
    readonly IHubContext<NotificationsHub, IHubNotification> _notifHub;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationDbContext _context;
    private readonly IGroupsService _groupsService;
    public NotificationService(IHubContext<NotificationsHub, IHubNotification> notifHub, IHttpContextAccessor httpContextAccessor, IApplicationDbContext context, IGroupsService groupsService)
    {
        this._notifHub = notifHub;
        this._httpContextAccessor = httpContextAccessor;
        _context = context;
        _groupsService = groupsService;
    }
    public async Task SendNotificationToUser(NotificationDTO notification, long? id)
    {
        if (id.HasValue)
        {
            await _notifHub.Clients.Group(id.Value.ToString()).SendNotif(notification);
            await SaveNotificationForUser(notification, id);
        }
    }

    public async Task SendNotificationToGroup(NotificationDTO notification, string groupName)
    {
        await this._notifHub.Clients.Group(groupName).SendNotif(notification);
        foreach(var UserId in await _groupsService.GetUsersFromGroup(groupName))
        {
            await SaveNotificationForUser(notification, UserId);
        }
    }

    public async Task SaveNotificationForUser(NotificationDTO notification, long? id)
    {
        if (id.HasValue)
        {
            Notification newNotif = new Notification()
            {
                Message = notification.Message,
                Type = notification.type,
                UserId = id.Value
            };
            await this._context.Notifications.Insert(newNotif);
            await this._context.SaveChangesAsync();
        }
    }
}
