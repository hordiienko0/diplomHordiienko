using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Buildings.Commands.DeleteBuilding;
public record DeleteBuildingCommand(long Id) : IRequest<Unit>;

public class DeleteBuildingCommandHandler : IRequestHandler<DeleteBuildingCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteBuildingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
    {
        bool result = await _context.Buildings.DeleteById(request.Id);
        if (!result)
        {
            throw new NotFoundException();
        }
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}
