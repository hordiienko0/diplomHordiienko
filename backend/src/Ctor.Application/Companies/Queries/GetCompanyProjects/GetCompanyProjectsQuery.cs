using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Models;
using Ctor.Application.DTOs;
using Ctor.Application.Projects.Queries.GetProjectsQuery;
using Ctor.Domain.Entities;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ctor.Application.Companies.Queries.GetCompanyProjects;

public record GetCompanyProjectsQuery(long id , PaginationQueryModelDTO QueryModel) : IRequest<PaginationModel<CompanyProjectDTO>>;

public class GetCompanyProjectsQueryHandler : IRequestHandler<GetCompanyProjectsQuery, PaginationModel<CompanyProjectDTO>>
{

    private readonly IApplicationDbContext _context;

    public GetCompanyProjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginationModel<CompanyProjectDTO>> Handle(GetCompanyProjectsQuery request, CancellationToken cancellationToken)
    {
        var query = request.QueryModel.Query;

        Expression<Func<Project, bool>> filterPredicate = project
            => (string.IsNullOrWhiteSpace(query) || project.ProjectName.ToLower().Contains(query.ToLower()))
                && project.CompanyId == request.id;

        var sort = request.QueryModel.Sort;
        var order = request.QueryModel.Order;
        var page = request.QueryModel.Page;
        var count = request.QueryModel.Count;

        var (projects, total) = await _context.Projects.GetFilteredWithTotalSum<CompanyProjectDTO>(filterPredicate, page, count, sort, order);

        return new() { List = projects, Total = total };
    }
}
