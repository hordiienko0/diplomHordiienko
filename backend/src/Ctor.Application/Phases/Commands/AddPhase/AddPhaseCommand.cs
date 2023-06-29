using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Phases.Commands.AddPhase;
public record AddPhaseCommand : IRequest<Unit>
{
    public long ProjectId { get; set; }
    public string PhaseName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<PhaseStepPostDto> PhaseSteps {get;set;}
}

public class AddPhaseCommandHandler : IRequestHandler<AddPhaseCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddPhaseCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(AddPhaseCommand request, CancellationToken cancellationToken)
    {
        var phaseSteps = request.PhaseSteps.Select(step => new PhaseStep()
        {
            PhaseStepName = step.PhaseStepName,
            IsDone = step.IsDone,
            BuildingId = step.BuildingId,
            StartDate = step.StartDate ?? request.StartDate,
            EndDate = step.EndDate ?? request.EndDate,
        }).ToList();

        var phase = new Phase
        {
            PhaseName = request.PhaseName,
            StartTime = request.StartDate,
            EndTime = request.EndDate,
            ProjectId = request.ProjectId,
            PhaseStep = 0,
            PhaseSteps = phaseSteps
        };

        await _context.Phases.Insert(phase);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
