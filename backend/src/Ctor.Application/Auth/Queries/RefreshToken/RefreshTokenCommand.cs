using Ctor.Application.Auth.Interfaces;
using MediatR;

namespace Ctor.Application.Auth.Queries.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<RefreshTokenDto>;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
{
    private readonly IUserManager _userManager;

    public RefreshTokenCommandHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var accessToken = await _userManager.RefreshAccessTokenAsync(
            request.AccessToken, request.RefreshToken, cancellationToken);

        return new RefreshTokenDto(accessToken.Value, accessToken.ExpiresAt);
    }
}