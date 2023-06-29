using Ctor.Application.Auth.Interfaces;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Auth.Queries.ChangePassword;

public record ChangePasswordCommand(string CurrentPassword, string NewPassword) : IRequest<ChangePasswordDto>;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordDto>
{
    private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;

    public ChangePasswordCommandHandler(IUserManager userManager, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
    }

    public async Task<ChangePasswordDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        await _userManager.ChangePasswordAsync(
            _currentUserService.Id!.Value, request.CurrentPassword, request.NewPassword, cancellationToken);

        return new ChangePasswordDto();
    }
}