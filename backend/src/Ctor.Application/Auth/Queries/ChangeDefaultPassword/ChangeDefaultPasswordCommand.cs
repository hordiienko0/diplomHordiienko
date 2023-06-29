using Ctor.Application.Auth.Interfaces;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Auth.Queries.ChangeDefaultPassword;

public record ChangeDefaultPasswordCommand(string NewPassword) : IRequest<ChangeDefaultPasswordDto>;

public class ChangeDefaultPasswordCommandHandler
    : IRequestHandler<ChangeDefaultPasswordCommand, ChangeDefaultPasswordDto>
{
    private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;

    public ChangeDefaultPasswordCommandHandler(IUserManager userManager, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
    }

    public async Task<ChangeDefaultPasswordDto> Handle(
        ChangeDefaultPasswordCommand request,
        CancellationToken cancellationToken)
    {
        await _userManager.ChangeDefaultPasswordAsync(
            _currentUserService.Id!.Value, request.NewPassword, cancellationToken);

        return new ChangeDefaultPasswordDto();
    }
}