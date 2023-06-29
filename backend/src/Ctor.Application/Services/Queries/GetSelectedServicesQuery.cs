using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Services.Queries;
public record GetSelectedServicesQuery(long BuildingId) : IRequest<List<ServiceDto>>;

public class GetSelectedServicesQueryHandler : IRequestHandler<GetSelectedServicesQuery, List<ServiceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSelectedServicesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<ServiceDto>> Handle(GetSelectedServicesQuery request, CancellationToken cancellationToken)
    {
        var allBuildingServices = await _context.RequiredServices.Get(b => b.BuildingId == request.BuildingId);
        var selectedServices = new List<ServiceDto>();
        foreach (var service in allBuildingServices)
        {
            selectedServices.Add(await _context.Vendors.GetById<ServiceDto>(service.VendorId, cancellationToken));
        }
        return selectedServices;
    }
}
