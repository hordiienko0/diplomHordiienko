using Ctor.Application.Roles.Queries;
using Ctor.Application.Users.Commands;
using Ctor.Application.Users.Queries;
using Ctor.Application.Users.Queries.GetCompanyUsers;
using Ctor.Application.Users.Queries.GetUsersDetailsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

[Authorize]
[Route("api/users")]
public class UserController : ApiControllerBase
{
    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetCurrentUserDetails(int id)
    {
        return Ok(await Mediator.Send(new GetUsersDetailsQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Add([FromBody] AddUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IEnumerable<string>> AddSeveralUsers([FromForm] IFormFile file, [FromForm] long companyId)
    {
        return await Mediator.Send(new AddSeveralUsersCommand
        {
            File = file.OpenReadStream(), //
            CompanyId = companyId
        });
    }

    [HttpGet]
    [Route("roles")]
    public async Task<List<RoleDto>> GetAllRoles()
    {
        return await Mediator.Send(new GetRolesQuery());
    }

    [HttpGet("byCompanyId/{id:long}")]
    public async Task<ActionResult<List<UserByCompanyIdResponseDto>>> GetUsersByCompanyId(long id)
    {
        return await Mediator.Send(new GetUsersByCompanyIdQuery(id));
    }

    [HttpGet("")]
    public async Task<ActionResult<List<UserByCompanyIdResponseDto>>> GetUsers([FromQuery] GetCompanyUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}