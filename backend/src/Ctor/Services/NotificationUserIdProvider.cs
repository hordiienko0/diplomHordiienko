using Microsoft.AspNetCore.SignalR;

namespace Ctor.Services;

public class NotificationUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
        {
        return connection.ConnectionId;
        //return connection.User?.Identity.Name;
    }
}
