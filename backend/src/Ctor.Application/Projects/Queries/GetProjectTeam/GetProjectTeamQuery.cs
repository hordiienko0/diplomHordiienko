using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities.Enums;
using MediatR;

namespace Ctor.Application.Projects.Queries.GetProjectTeam;

public record GetProjectTeamQuery(long ProjectId) : IRequest<GetProjectTeamDto>;

public class GetProjectTeamQueryHandler : IRequestHandler<GetProjectTeamQuery, GetProjectTeamDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetProjectTeamQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<GetProjectTeamDto> Handle(GetProjectTeamQuery request, CancellationToken cancellationToken)
    {
        var canSee = await _context.Users.AnyAsync(
            u => u.Company.Users.Any(user => user.Id == _currentUserService.Id &&
                                             (user.Role.Type == UserRoles.OperationalManager ||
                                              user.Role.Type == UserRoles.ProjectManager))
                 && u.Company.Projects.Any(p => p.Id == request.ProjectId));

        if (!canSee)
        {
            throw new ForbiddenAccessException();
        }

        return await _context.Projects.GetById<GetProjectTeamDto>(request.ProjectId, cancellationToken);
    }
}