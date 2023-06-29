using AutoMapper;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Resources.Materials.Queries.GetMeasurementQuery;
public record GetMeasurementQuery : IRequest<List<GetMeasurementQueryDto>>;

public class GetMeasurementQueryHandler : IRequestHandler<GetMeasurementQuery, List<GetMeasurementQueryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMeasurementQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetMeasurementQueryDto>> Handle(GetMeasurementQuery request, CancellationToken cancellationToken)
    {
        var materilType = await _context.Measurements.GetAll();

        return _mapper.Map<List<GetMeasurementQueryDto>>(materilType);
    }
}