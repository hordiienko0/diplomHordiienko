using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Services.Commands;
public class AddServiceToBuildingCommand : IRequest<List<ServiceDto>>
{
    public long BuildingId { get; set; }
    public List<long> ServiceIds { get; set; }
}

public class AddServiceToBuildingCommandHandler : IRequestHandler<AddServiceToBuildingCommand, List<ServiceDto>>
{
    private readonly IApplicationDbContext _context;

    public AddServiceToBuildingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<ServiceDto>> Handle(AddServiceToBuildingCommand request, CancellationToken cancellationToken)
    {
        foreach(var id in request.ServiceIds) {
            await _context.RequiredServices.Insert(new RequiredService()
            {
                BuildingId = request.BuildingId,
                VendorId = id
            });
        }
        await _context.SaveChangesAsync(cancellationToken);
        var allBuildingServices = (await _context.RequiredServices.Get(b => b.BuildingId == request.BuildingId));
        var services = new List<ServiceDto>();
        foreach(var service in allBuildingServices)
        {
            services.Add(await _context.Vendors.GetById<ServiceDto>(service.VendorId, cancellationToken));
        }
        return services;
    }
}
