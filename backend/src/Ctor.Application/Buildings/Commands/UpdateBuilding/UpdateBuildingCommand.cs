using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Buildings.Commands.UpdateBuilding;
public record UpdateBuildingCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public string BuildingName { get; set; } = string.Empty;
}

public class UpdateBuildingCommandHandler : IRequestHandler<UpdateBuildingCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateBuildingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
    {
        var building = await _context.Buildings.GetById(request.Id);
        if (building == null)
        {
            throw new NotFoundException();
        }
        building.BuildingName = request.BuildingName;
        _context.Buildings.Update(building);
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}
