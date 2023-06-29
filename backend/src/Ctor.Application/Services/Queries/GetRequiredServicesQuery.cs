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
public record GetRequiredServicesQuery : IRequest<List<ServiceDto>>
{
    public string? Filter { get; set; }
    public long BuildingId { get; set; }

    public GetRequiredServicesQuery(string filter, long buildingId)
    {
        Filter = filter;
        BuildingId = buildingId;
    }

}
public class GetRequiredServicesQueryHandler : IRequestHandler<GetRequiredServicesQuery, List<ServiceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public GetRequiredServicesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    public async Task<List<ServiceDto>> Handle(GetRequiredServicesQuery request, CancellationToken cancellationToken)
    {
        var allBuildingServices = (await _context.RequiredServices.Get(b => b.BuildingId == request.BuildingId));
        var selectedServices = new List<ServiceDto>();
        foreach (var service in allBuildingServices)
        {
            selectedServices.Add(await _context.Vendors.GetById<ServiceDto>(service.VendorId, cancellationToken));
        }
        Expression<Func<Vendor, bool>> filterPredicate = service => service.CompanyId == _currentUserService.CompanyId;
        if (!string.IsNullOrEmpty(request.Filter))
        {
            filterPredicate = service => (service.VendorName.ToLower().StartsWith(request.Filter.ToLower())
            || service.VendorTypes.Any(s=>s.Name.ToLower().StartsWith(request.Filter.ToLower())))
            && service.CompanyId == _currentUserService.CompanyId;
        }
        var services = await _context.Vendors.Get<ServiceDto>(filterPredicate);
       
        var s = services.AsQueryable().ExceptBy(selectedServices.Select(s=>s.Id), s=>s.Id).ToList();
        return s;

        //Expression<Func<Vendor, bool>> filterPredicate = service => service.CompanyId == _currentUserService.CompanyId;
        //if (!string.IsNullOrEmpty(request.Filter))
        //{
        //    filterPredicate = service => service.VendorName.ToLower().StartsWith(request.Filter.ToLower())
        //    && service.CompanyId == _currentUserService.CompanyId;
        //}
        //var services = (await _context.RequiredServices.Get(s => s.BuildingId == request.BuildingId)).Select(async x => (await _context.Vendors.GetById<ServiceDto>(x.VendorId, cancellationToken)));
        //return (await Task.WhenAll(services)).ToList();
        //return await _context.Vendors.GetOrdered<ServiceDto>(request.Sort, Order.ASC, filterPredicate);
    }
}
