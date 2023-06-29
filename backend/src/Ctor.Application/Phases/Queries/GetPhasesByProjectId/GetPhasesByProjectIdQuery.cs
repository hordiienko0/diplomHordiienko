using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Phases.Queries.GetPhasesByProjectId;
public record GetPhasesByProjectIdQuery(long ProjectId) : IRequest<List<PhaseDto>>;

public class GetPhasesByProjectIdQueryHandler : IRequestHandler<GetPhasesByProjectIdQuery, List<PhaseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPhasesByProjectIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<PhaseDto>> Handle(GetPhasesByProjectIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Phases.Get<PhaseDto>(phase => phase.ProjectId == request.ProjectId);
    }
}
