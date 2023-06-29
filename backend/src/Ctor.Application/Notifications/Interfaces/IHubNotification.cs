using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.DTOs;

namespace Ctor.Application.Notifications.Interfaces;
public interface IHubNotification
{
    Task SendNotif(NotificationDTO notification);
}
