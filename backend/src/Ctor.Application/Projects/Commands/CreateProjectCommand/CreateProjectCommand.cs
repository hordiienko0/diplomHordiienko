using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;
using MediatR;

namespace Ctor.Application.Projects.Commands.CreateProjectCommand;

public record CreateProjectCommand(CreateProjectDTO Project) : IRequest;

public class CreateCompanyCommandHandler : IRequestHandler<CreateProjectCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly INumberGenerateService _numberGenerateService;
    private readonly ICurrentUserService _currentUserService;
    private readonly INotificationService _notifService;

    public CreateCompanyCommandHandler(
        IApplicationDbContext context,
        IMapper mapper,
        INumberGenerateService numberGenerateService,
        ICurrentUserService currentUserService,
        INotificationService notifService)
    {
        _context = context;
        _mapper = mapper;
        _numberGenerateService = numberGenerateService;
        _currentUserService = currentUserService;
        _notifService = notifService;
    }

    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        if (_currentUserService.Role != UserRoles.OperationalManager)
        {
            throw new ForbiddenAccessException();
        }

        var project = _mapper.Map<Project>(request.Project);

        project.UserId = _currentUserService.Id;
        project.CompanyId = _currentUserService.CompanyId!.Value;

        if (await _context.Projects.AnyAsync(x => x.ProjectId == project.ProjectId))
        {
            project.ProjectId = _numberGenerateService.GetRandomNumberForId();
        }

        await _context.Projects.AddRangeAsync(project);
        await _context.SaveChangesAsync(cancellationToken);

        await _notifService.SendNotificationToGroup(
            new DTOs.NotificationDTO
            {
                type = "info",
                Message = "OM with id:" + _currentUserService.Id + " just created project: " + project.ProjectName
            }, "admin");

        await _notifService.SendNotificationToUser(
            new DTOs.NotificationDTO
            {
                type = "success", Message = "You successfully created project: " + project.ProjectName
            }, _currentUserService.Id);

        return Unit.Value;
    }
}