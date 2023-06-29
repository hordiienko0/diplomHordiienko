using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.BuildingBlocks.Commands.UpdateBuildingBlock;
public record UpdateBuildingBlockCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public bool IsDone { get; set; }
}

public class UpdateBuildingBlockCommandHandler : IRequestHandler<UpdateBuildingBlockCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateBuildingBlockCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBuildingBlockCommand request, CancellationToken cancellationToken)
    {
        var buildingBlock = await _context.BuildingBlocks.GetById(request.Id);
        if (buildingBlock == null)
        {
            throw new NotFoundException();
        }
        buildingBlock.IsDone = request.IsDone;
        _context.BuildingBlocks.Update(buildingBlock);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
