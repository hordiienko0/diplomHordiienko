using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Phases.Commands.DeletePhase;
public record DeletePhaseCommand(long Id) : IRequest<Unit>;

public class DeletePhaseCommandHadnler : IRequestHandler<DeletePhaseCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeletePhaseCommandHadnler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeletePhaseCommand request, CancellationToken cancellationToken)
    {
        var phase = await _context.Phases.GetById(request.Id);
        _context.Phases.Delete(phase);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

