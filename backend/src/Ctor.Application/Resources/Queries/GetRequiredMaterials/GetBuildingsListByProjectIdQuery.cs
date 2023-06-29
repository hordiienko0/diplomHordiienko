using AutoMapper;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Resources.Queries.GetRequiredMaterials;

public record GetRequiredMaterialsQuery(long BuildingId) : IRequest<GetRequiredMaterialsDto>;

public class GetRequiredMaterialsQueryHandler : IRequestHandler<GetRequiredMaterialsQuery, GetRequiredMaterialsDto>
{
    private readonly IApplicationDbContext _context;

    public GetRequiredMaterialsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetRequiredMaterialsDto> Handle(GetRequiredMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        return new GetRequiredMaterialsDto
        {
            RequiredMaterials = await _context.RequiredMaterials.Get<GetRequiredMaterialDto>(
                m => m.BuildingId == request.BuildingId),
        };
    }
}