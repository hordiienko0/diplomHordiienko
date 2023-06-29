using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Notifications.Queries.GetNotifListByUserId;
public class NotificationDto : IMapFrom<Notification>
{
    public long Id { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationDto>();
    }
}
