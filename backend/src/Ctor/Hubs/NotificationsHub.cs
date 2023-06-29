namespace Ctor.Hubs;

using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Notifications.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

public class NotificationsHub : Hub<IHubNotification>
{
    readonly IGroupsService _groupsService;
    readonly ICurrentUserService _currentUserService;
    readonly IHttpContextAccessor _contextAccessor;
    public NotificationsHub(IGroupsService groupsService, ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor)
    {
        this._groupsService = groupsService;
        this._currentUserService = currentUserService;
        this._contextAccessor = httpContextAccessor;
    }
    public override async Task OnConnectedAsync()
    {
        try
        {
            var httpContext = this._contextAccessor.HttpContext;
            var userid = httpContext.Request.Query["userid"];
            int test = Context.Items.Count;
            await Groups.AddToGroupAsync(Context.ConnectionId, userid);
            
            foreach (string group in await _groupsService.GetGroupsOfUserAsync((long)Convert.ToDouble(userid)))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
            }
        } catch(Exception ex)
        {
            return;
        }
    }

    public string GetUserId()
    {
        return Context.UserIdentifier;
    }
}
