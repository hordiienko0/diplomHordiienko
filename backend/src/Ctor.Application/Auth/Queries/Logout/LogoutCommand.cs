using Ctor.Application.Auth.Interfaces;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Auth.Queries.Logout;

public record LogoutCommand : IRequest<LogoutDto>;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutDto>
{
    private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;

    public LogoutCommandHandler(IUserManager userManager, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
    }

    public async Task<LogoutDto> Handle(LogoutCommand request, CancellationToken ct)
    {
        if (_currentUserService.Id is not null)
        {
            await _userManager.LogoutAsync(_currentUserService.Id!.Value, ct);
        }

        return new LogoutDto();
    }
}