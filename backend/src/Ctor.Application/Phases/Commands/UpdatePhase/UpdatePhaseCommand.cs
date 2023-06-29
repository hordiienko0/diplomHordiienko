using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Phases.Commands.UpdatePhase;
public record UpdatePhaseCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public string PhaseName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class UpdatePhaseCommandHandler : IRequestHandler<UpdatePhaseCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdatePhaseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdatePhaseCommand request, CancellationToken cancellationToken)
    {
        var phaseToUpdate = await _context.Phases.GetById(request.Id);
        phaseToUpdate.PhaseName = request.PhaseName;
        phaseToUpdate.StartTime = request.StartDate;
        phaseToUpdate.EndTime = request.EndDate;

        var outOfLowerDate = await _context.PhaseSteps.Get(step => step.StartDate < request.StartDate);
        outOfLowerDate = outOfLowerDate.Select(step => new PhaseStep
        {
            Id = step.Id,
            PhaseId = step.PhaseId,
            PhaseStepName = step.PhaseStepName,
            StartDate = request.StartDate,
            EndDate = step.EndDate,
            IsDone = step.IsDone,
            BuildingId = step.BuildingId
        }).ToList();
        foreach(var step in outOfLowerDate)
        {
            _context.PhaseSteps.Update(step);
        }

        var outOfUpperDate = await _context.PhaseSteps.Get(step => step.EndDate > request.EndDate);
        outOfUpperDate = outOfUpperDate.Select(step => new PhaseStep
        {
            Id = step.Id,
            PhaseId = step.PhaseId,
            PhaseStepName = step.PhaseStepName,
            StartDate = step.StartDate,
            EndDate = request.EndDate,
            IsDone = step.IsDone,
            BuildingId = step.BuildingId
        }).ToList();
        foreach (var step in outOfUpperDate)
        {
            _context.PhaseSteps.Update(step);
        }

        _context.Phases.Update(phaseToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}