using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Phases.Commands.UpdatePhaseSteps;
public record UpdatePhaseStepsCommand : IRequest<Unit>
{
    public long PhaseId { get; set; }
    public List<PhaseStepPutDto> PhaseSteps { get; set; }
}

public class UpdatePhaseStepsCommandHandler : IRequestHandler<UpdatePhaseStepsCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePhaseStepsCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdatePhaseStepsCommand request, CancellationToken cancellationToken)
    {
        var oldPhaseSteps = await _context.PhaseSteps.Get(x => x.PhaseId == request.PhaseId);

        var oldPhaseStepIdsToRemove = oldPhaseSteps.Select(x => x.Id).Except(request.PhaseSteps.Where(x => x.Id.HasValue).Select(x => x.Id.Value));

        foreach(var id in oldPhaseStepIdsToRemove)
        {
            var stepToRemove = await _context.PhaseSteps.GetById(id);
            _context.PhaseSteps.Delete(stepToRemove);
        }

        var newPhaseStepsToAdd = _mapper.Map<List<PhaseStep>>(request.PhaseSteps.Where(x => !x.Id.HasValue).ToList());
        var phase = await _context.Phases.GetById(request.PhaseId);
        foreach(var newStep in newPhaseStepsToAdd)
        {
            newStep.PhaseId = request.PhaseId;
            if (newStep.BuildingId.HasValue)
            {
                newStep.StartDate = phase.StartTime;
                newStep.EndDate = phase.EndTime;
            }
            await _context.PhaseSteps.Insert(newStep);
        }


        var editedPhaseSteps = request.PhaseSteps.Where(x => oldPhaseSteps.Any(y => y.Id == x.Id));

        foreach(var step in editedPhaseSteps)
        {
            var stepToUpdate = await _context.PhaseSteps.GetById(step.Id!.Value);
            _context.PhaseSteps.Update(stepToUpdate);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

