using Ctor.Application.Auth.Interfaces;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Auth.Queries.KeepDefaultPassword;

public record KeepDefaultPasswordCommand : IRequest<KeepDefaultPasswordDto>;

public class KeepDefaultPasswordCommandHandler : IRequestHandler<KeepDefaultPasswordCommand, KeepDefaultPasswordDto>
{
    private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;

    public KeepDefaultPasswordCommandHandler(IUserManager userManager, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
    }

    public async Task<KeepDefaultPasswordDto> Handle(
        KeepDefaultPasswordCommand request,
        CancellationToken cancellationToken)
    {
        await _userManager.KeepDefaultPasswordAsync(_currentUserService.Id!.Value, cancellationToken);

        return new KeepDefaultPasswordDto();
    }
}