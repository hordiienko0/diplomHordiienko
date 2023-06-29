using AutoMapper;
using Ctor.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ctor.Application.MyEntity.Queries;

public record GetMyEntitiesQuery : IRequest<MyEntityDto>
{
    public GetMyEntitiesQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public class GetMyEntitiesQueryHandler : IRequestHandler<GetMyEntitiesQuery, MyEntityDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetMyEntitiesQueryHandler> _logger;

    public GetMyEntitiesQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetMyEntitiesQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<MyEntityDto> Handle(GetMyEntitiesQuery request, CancellationToken cancellationToken)
    {
        
        _logger.LogInformation("In query");
        return new MyEntityDto
        {
            Name = "Outer text",
            Detail = new MyEntityDetailDto
            {
                Detail = "Inner Text"
            }
        };

    }
}
