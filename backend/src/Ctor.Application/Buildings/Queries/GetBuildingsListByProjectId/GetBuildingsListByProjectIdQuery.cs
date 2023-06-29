using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Buildings.Queries.GetBuildingsListByProjectId;
public record GetBuildingsListByProjectIdQuery(long ProjectId) : IRequest<List<BuildingDto>>;

public class GetBuildingsListByProjectIdQueryHandler : IRequestHandler<GetBuildingsListByProjectIdQuery, List<BuildingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBuildingsListByProjectIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BuildingDto>> Handle(GetBuildingsListByProjectIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Buildings.Get<BuildingDto>(b => b.ProjectId == request.ProjectId);
    }
}

