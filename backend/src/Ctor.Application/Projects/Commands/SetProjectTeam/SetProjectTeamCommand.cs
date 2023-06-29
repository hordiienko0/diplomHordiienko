using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities.Enums;
using MediatR;

namespace Ctor.Application.Projects.Commands.SetProjectTeam;

public record SetProjectTeamCommand(long ProjectId, ISet<long> UserIds) : IRequest;

public class SetProjectTeamCommandHandler : IRequestHandler<SetProjectTeamCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly INotificationService _notifService;
    

    public SetProjectTeamCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        INotificationService notifService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _notifService = notifService;
        
    }

    public async Task<Unit> Handle(SetProjectTeamCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.GetById(request.ProjectId);
        foreach(long id in request.UserIds)
        {
            await _notifService.SendNotificationToUser(new DTOs.NotificationDTO
            {
                Message = "You were added to project: " + project.ProjectName,
                type = NotificationTypes.Info
            }, id);
        }
        await _notifService.SendNotificationToUser(new DTOs.NotificationDTO
        {
            Message = "You successful added users to project.",
            type = NotificationTypes.Success
        }, _currentUserService.Id);

        await _context.Projects.SetTeamAsync(request.ProjectId, request.UserIds, _currentUserService.Id!.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}