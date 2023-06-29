using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities.Enums;
using MediatR;

namespace Ctor.Application.Projects.Commands.ChangeStatus;

public record ChangeStatusCommand(long ProjectId, ProjectStatus NewStatus) : IRequest;

public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly INotificationService _notifService;

    public ChangeStatusCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        INotificationService notifService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _notifService = notifService;
    }

    public async Task<Unit> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .SingleOrDefault(p => p.Id == request.ProjectId && p.Company.Users
                .Any(u => u.Role.Type == UserRoles.OperationalManager || u.Role.Type == UserRoles.ProjectManager));

        if (project == null)
        {
            throw new NotFoundException();
        }

        project.Status = request.NewStatus;
        await _context.SaveChangesAsync(cancellationToken);

        await _notifService.SendNotificationToGroup(new DTOs.NotificationDTO
        {
            type = "info",
            Message = "Manager with id:" + _currentUserService.Id + " janged status for project " + project.ProjectName
        }, "admin");

        await _notifService.SendNotificationToUser(new DTOs.NotificationDTO
        {
            type = "success",
            Message = "You successfuly changed status: " + project.ProjectName
        }, _currentUserService.Id);

        return Unit.Value;
    }
}