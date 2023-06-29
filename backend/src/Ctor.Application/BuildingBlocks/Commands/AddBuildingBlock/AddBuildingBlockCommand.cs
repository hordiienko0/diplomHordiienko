using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.BuildingBlocks.Commands.AddBuildingBlock;
public record AddBuildingBlockCommand : IRequest<Unit>
{
    public string BuildingBlockName { get; set; } = string.Empty;
    public long BuildingId { get; set; }
}

public class AddBuildingBlockCommandHandler : IRequestHandler<AddBuildingBlockCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public AddBuildingBlockCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddBuildingBlockCommand request, CancellationToken cancellationToken)
    {
        var buildingBlock = new BuildingBlock
        {
            BuildingBlockName = request.BuildingBlockName,
            Details = "",
            BuildingId = request.BuildingId,
        };
        await _context.BuildingBlocks.Insert(buildingBlock);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
