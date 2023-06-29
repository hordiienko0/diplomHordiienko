using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.BuildingBlocks.Commands.DeleteBuildingBlock;
public record DeleteBuildingBlockCommand(long Id) : IRequest<Unit>;

public class DeleteBuildingBlockCommandHandler : IRequestHandler<DeleteBuildingBlockCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteBuildingBlockCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBuildingBlockCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.BuildingBlocks.DeleteById(request.Id);
        if (!result)
        {
            throw new NotFoundException();
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}