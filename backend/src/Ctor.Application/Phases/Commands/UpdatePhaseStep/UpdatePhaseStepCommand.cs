using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Phases.Commands.UpdatePhaseStep;
public record UpdatePhaseStepCommand :IRequest<Unit>
{
    public long Id { get; set; }
    public bool IsDone { get; set; }
}

public class UpdatePhaseStepCommandHandler : IRequestHandler<UpdatePhaseStepCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdatePhaseStepCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdatePhaseStepCommand request, CancellationToken cancellationToken)
    {
        var phaseStep = await _context.PhaseSteps.GetById(request.Id);
        phaseStep.IsDone = request.IsDone;
        _context.PhaseSteps.Update(phaseStep);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
