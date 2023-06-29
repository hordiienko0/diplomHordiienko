using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.DTOs;

namespace Ctor.Application.Common.Interfaces;
public interface INotificationService
{
    public Task SendNotificationToUser(NotificationDTO notification, long? id);
    public Task SendNotificationToGroup(NotificationDTO notification, string groupName);
}
