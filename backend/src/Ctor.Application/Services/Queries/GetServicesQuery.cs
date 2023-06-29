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

namespace Ctor.Application.Services.Queries;
public record GetServicesQuery: IRequest<IEnumerable<ServiceDto>>
{
    public string Filter { get; set; }
    public string Sort { get; set; }

    public GetServicesQuery(string filter, string sort)
    {
        Filter = filter;
        Sort = sort;
    }

}
public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, IEnumerable<ServiceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public GetServicesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Vendor, bool>> filterPredicate = service => service.CompanyId == _currentUserService.CompanyId;
        if (!string.IsNullOrEmpty(request.Filter))
        {
            filterPredicate = service => service.VendorName.ToLower().StartsWith(request.Filter.ToLower()) 
            && service.CompanyId == _currentUserService.CompanyId;
        }
        //return await _context.RequiredServices.
        return await _context.Vendors.GetOrdered<ServiceDto>(request.Sort, Order.ASC, filterPredicate);
    }
}