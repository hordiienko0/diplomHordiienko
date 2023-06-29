using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;
using Ctor.Application.Common.Exceptions;

namespace Ctor.Application.Resources.Materials.Queries.GetAvailableMaterialsForProjectQuery;


public record GetAvailableMaterialsForProjectQuery(string? Filter) : IRequest<List<GetAvailableMaterialsForProjectDto>>;
public class GetAvailableMaterialsForProjectQueryHandler : IRequestHandler<GetAvailableMaterialsForProjectQuery, List<GetAvailableMaterialsForProjectDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetAvailableMaterialsForProjectQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<GetAvailableMaterialsForProjectDto>> Handle(GetAvailableMaterialsForProjectQuery request, CancellationToken cancellationToken)
    {
        List<GetAvailableMaterialsForProjectDto> result = new List<GetAvailableMaterialsForProjectDto>();

        var materials = await _context.Materials.Get(el => el.CompanyId == _currentUserService.CompanyId);
        
        if (!materials.Any()) throw new NotFoundException("There is no available materials");
        
        var required = await _context.RequiredMaterials.Get(el => el.Building.Project.CompanyId == _currentUserService.CompanyId);
        foreach(var material in materials)
        {
            var sumUsed = required.Where(el => el.MaterialId == material.Id).Select(el => el.Amount).Sum();
            var diff = material.Amount - sumUsed;
            if(diff > 0)
            {
                var type = await _context.MaterialType.FirstOrDefault(el => el.Id == material.MaterialTypeId, cancellationToken);
                var msr = await _context.Measurements.FirstOrDefault(el => el.Id == material.MeasurementId, cancellationToken);
                if (msr == null || type == null) continue;
                if (request.Filter != null && !type.Name.ToLower().Contains(request.Filter.ToLower())) continue; 
                result.Add(new GetAvailableMaterialsForProjectDto()
                {
                    Amount = diff,
                    CompanyAddress = material.CompanyAddress,
                    CompanyName = material.CompanyName,
                    Id = material.Id,
                    MaterialTypeName = type.Name,
                    MeasurementName = msr.Name,
                    Price = material.Price
                });
            }
        }
        return result;
    }
}