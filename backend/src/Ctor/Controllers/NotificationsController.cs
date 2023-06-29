using Ctor.Application.Notifications.Queries.GetNotifListByUserId;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ctor.Application.Projects.Queries.GetProjectsQuery;
using Ctor.Application.Notifications.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Ctor.Controllers;

[ApiController]
[Route("api/notifi")]
[Authorize(AuthenticationSchemes = "Bearer")]

public class NotificationsController : ApiControllerBase
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllNotificationsForUser()
    {
        return Ok(await Mediator.Send(new GetNotifListByUserIdQuery()));
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteNotificationById(long id)
    {
        await Mediator.Send(new DeleteNotificationByIdCommand(id));
        return NoContent();
    }

    [HttpDelete("deleteall/{id:long}")]
    public async Task<IActionResult> DeleteAllNotificationForUser(long id)
    {
        await Mediator.Send(new DeleteAllNotificationByUserIdCommand(id));
        return NoContent();
    }
}
