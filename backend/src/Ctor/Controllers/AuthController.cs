using Ctor.Application.Auth.Queries.ChangeDefaultPassword;
using Ctor.Application.Auth.Queries.ChangePassword;
using Ctor.Application.Auth.Queries.ForgotPassword;
using Ctor.Application.Auth.Queries.KeepDefaultPassword;
using Ctor.Application.Auth.Queries.Login;
using Ctor.Application.Auth.Queries.Logout;
using Ctor.Application.Auth.Queries.RefreshToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class AuthController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginDto>> Login([FromBody] LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("logout")]
    public async Task<ActionResult<LogoutDto>> Logout([FromBody] LogoutCommand command)
    {
        return await Mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<RefreshTokenDto>> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<ChangePasswordDto>> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("change-default-password")]
    public async Task<ActionResult<ChangeDefaultPasswordDto>> ChangeDefaultPassword(
        [FromBody] ChangeDefaultPasswordCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("keep-default-password")]
    public async Task<ActionResult<KeepDefaultPasswordDto>> KeepDefaultPassword(
        [FromBody] KeepDefaultPasswordCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}