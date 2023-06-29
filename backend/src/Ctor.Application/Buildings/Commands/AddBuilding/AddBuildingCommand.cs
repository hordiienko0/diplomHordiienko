using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Buildings.Commands.AddBuilding;
public record AddBuildingCommand : IRequest<Unit>
{
    public string BuildingName { get; set; }
    public long ProjectId { get; set; }
}

public class AddBuildingCommandHandler : IRequestHandler<AddBuildingCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public AddBuildingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddBuildingCommand request, CancellationToken cancellationToken)
    {
        var building = new Building()
        {
            BuildingName = request.BuildingName,
            ProjectId = request.ProjectId,
        };

        await _context.Buildings.Insert(building);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
