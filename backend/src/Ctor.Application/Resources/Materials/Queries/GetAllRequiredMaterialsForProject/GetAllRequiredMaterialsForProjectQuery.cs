using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Resources.Materials.Queries.GetAllRequiredMaterialsForProject;

public record GetAllRequiredMaterialsForProjectQuery(long projectId, string? sort, string? filter) : IRequest<List<GetAllRequiredMaterialsForProjectDto>>;
public class GetAllRequiredMaterialsForProjectQueryHandler : IRequestHandler<GetAllRequiredMaterialsForProjectQuery, List<GetAllRequiredMaterialsForProjectDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetAllRequiredMaterialsForProjectQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<GetAllRequiredMaterialsForProjectDto>> Handle(GetAllRequiredMaterialsForProjectQuery request, CancellationToken cancellationToken)
    {
        List<GetAllRequiredMaterialsForProjectDto> result = new List<GetAllRequiredMaterialsForProjectDto>();

        var buidings = await _context.Buildings.Get(el => el.ProjectId == request.projectId);
        if(buidings == null) return result;
        foreach(var buiding in buidings)
        {
            var requredMaterials = await _context.RequiredMaterials.Get(el => el.BuildingId == buiding.Id);
            if (requredMaterials == null) continue;
            foreach(var record in requredMaterials)
            {
                // check if material was already added to result
                GetAllRequiredMaterialsForProjectDto? model = result.FirstOrDefault(el => el.BuildingId == buiding.Id && el.MaterialId == record.MaterialId);
                if (model != null)
                {
                    model.Amount += record.Amount;
                    continue;
                 }
                // if not then add new record to result
                var material =  await _context.Materials.FirstOrDefault(el => el.Id == record.MaterialId);
                if (material == null) continue;
                var type = await _context.MaterialType.FirstOrDefault(el => el.Id == material.MaterialTypeId);
                var msr = await _context.Measurements.FirstOrDefault(el => el.Id == material.MaterialTypeId);
                model = new GetAllRequiredMaterialsForProjectDto()
                {
                    Amount = record.Amount,
                    BuildingName = buiding.BuildingName,
                    BuildingId = buiding.Id,
                    CompanyAddress = material?.CompanyAddress,
                    CompanyName = material?.CompanyName,
                    MaterialId = record.MaterialId,
                    MaterialTypeName = type?.Name,
                    MeasurementName = msr?.Name,
                };
                if (material != null) model.Price = material.Price;
                result.Add(model);
            }
        }
        return result;
    }
}