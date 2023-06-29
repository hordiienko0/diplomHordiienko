using AutoMapper;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Resources.Materials.Queries.GetMaterialTypeQuery;
public record GetMaterialTypeQuery : IRequest<List<GetMaterialTypeQueryDto>>;

public class GetMaterialTypeQueryHandler : IRequestHandler<GetMaterialTypeQuery, List<GetMaterialTypeQueryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMaterialTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetMaterialTypeQueryDto>> Handle(GetMaterialTypeQuery request, CancellationToken cancellationToken)
    {
        var materilType = await _context.MaterialType.GetAll();

        return _mapper.Map<List<GetMaterialTypeQueryDto>>(materilType);
    }
}