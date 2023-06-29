using Ctor.Application.Common.Events;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Interfaces.Bus;
using MediatR;

namespace Ctor.Application.Resources.Materials.Commands.CreateMaterialReport;

public record CreateMaterialReportCommand(long ProjectId): IRequest;

public class CreateMaterialReportCommandHandler : IRequestHandler<CreateMaterialReportCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventBus _bus;
    private readonly ICurrentUserService _currentUser;

    public CreateMaterialReportCommandHandler(IApplicationDbContext context, IEventBus bus, ICurrentUserService currentUser) 
    {
        _context = context;
        _bus = bus;
        _currentUser = currentUser;
    }
    public async Task<Unit> Handle(CreateMaterialReportCommand request, CancellationToken cancellationToken)
    {
        // Check if exists
        var project = await _context.Projects.GetById(request.ProjectId, cancellationToken);
        if (_currentUser.Id != null && _currentUser.CompanyId != null && _currentUser.CompanyId == project.CompanyId)
        {
            _bus.Publish(new CreateReportEvent(project.Id, (long)_currentUser.Id ));
        }
        else
        {
            throw new ArgumentException("Error with login");
        }

        return Unit.Value;
    }
}