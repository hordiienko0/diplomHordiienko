using Ctor.Application.Auth.Interfaces;
using MediatR;

namespace Ctor.Application.Auth.Queries.Login;

public record LoginCommand(string Email, string Password) : IRequest<LoginDto>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
{
    private readonly IUserManager _userManager;

    public LoginCommandHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken ct)
    {
        var result = await _userManager.LoginAsync(request.Email, request.Password, ct);

        return new LoginDto(
            new LoginUserDto(result.UserId, result.Email),
            askToChangePassword: result.AskToChangePassword,
            new LoginTokenDto(result.AccessToken.Value, result.AccessToken.ExpiresAt),
            new LoginTokenDto(result.RefreshToken.Value, result.RefreshToken.ExpiresAt));
    }
}