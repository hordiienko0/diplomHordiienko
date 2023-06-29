using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Enums;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Models;
using Ctor.Application.Companies.Queries;
using Ctor.Application.DTOs;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Projects.Queries.GetProjectsQuery;

public record GetProjectDetailedQuery(long id) : IRequest<ProjectDetailedDTO>;

public class GetProjectDetailedQueryHandler : IRequestHandler<GetProjectDetailedQuery, ProjectDetailedDTO>
{

    private readonly IApplicationDbContext _context;

    public GetProjectDetailedQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDetailedDTO> Handle(GetProjectDetailedQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects.FirstOrDefault<ProjectDetailedDTO>(p => p.Id == request.id, cancellationToken);
    }
}
