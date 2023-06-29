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

public record GetProjectsOverviewQuery(ProjectPaginationQueryDTO QueryModel) : IRequest<PaginationModel<ProjectOverviewDto>>;

public class GetProjectsOverviewQueryHandler : IRequestHandler<GetProjectsOverviewQuery, PaginationModel<ProjectOverviewDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetProjectsOverviewQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PaginationModel<ProjectOverviewDto>> Handle(GetProjectsOverviewQuery request, CancellationToken cancellationToken)
    {
        var query = request.QueryModel.Query;

        Expression<Func<Domain.Entities.Project, bool>> filterPredicate = project
            => (string.IsNullOrWhiteSpace(query) || project.ProjectName.ToLower().Contains(query.ToLower()))
               && (project.UserId == _currentUserService.Id!.Value || project.Assignees.Any(a => a.User.Id == _currentUserService.Id!.Value))
               && project.Status == request.QueryModel.Status;

        var sort = request.QueryModel.Sort;
        var order = request.QueryModel.Order;
        var page = request.QueryModel.Page;
        var count = request.QueryModel.Count;

        var (projects, total) = await _context.Projects
            .GetFilteredWithTotalSum<ProjectOverviewDto>(filterPredicate, page, count, sort, order);

        return new() { List = projects, Total = total };
    }
}