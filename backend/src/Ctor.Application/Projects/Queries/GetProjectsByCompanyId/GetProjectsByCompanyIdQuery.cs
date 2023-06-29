using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Projects.Queries.GetProjectsQuery;
using MediatR;

namespace Ctor.Application.Projects.Queries.GetProjectsByCompanyId;
public record GetProjectsByCompanyIdQuery(long Id) : IRequest<List<ProjectOverviewDto>>;


public class GetProjectsByCompanyIdQueryHandler : IRequestHandler<GetProjectsByCompanyIdQuery, List<ProjectOverviewDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectsByCompanyIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProjectOverviewDto>> Handle(GetProjectsByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects.Get<ProjectOverviewDto>(p => p.CompanyId == request.Id);
    }
}