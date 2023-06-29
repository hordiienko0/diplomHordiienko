using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Enums;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Companies.Queries.GetCompaniesOverview;

public record GetCompaniesOverviewQuery(string Filter, string Sort) : IRequest<List<CompanyOverviewDto>>;

public class GetCompaniesOverviewQueryHandler : IRequestHandler<GetCompaniesOverviewQuery, List<CompanyOverviewDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCompaniesOverviewQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<CompanyOverviewDto>> Handle(GetCompaniesOverviewQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Company, bool>> filterPredicate = null;
        if (!string.IsNullOrEmpty(request.Filter))
        {
            filterPredicate = company => company.CompanyName.ToLower().StartsWith(request.Filter.ToLower());
        }

        var companies = await _context.Companies.GetOrdered<CompanyOverviewDto>(request.Sort, Order.ASC, filterPredicate);

        return companies;
    }
}
