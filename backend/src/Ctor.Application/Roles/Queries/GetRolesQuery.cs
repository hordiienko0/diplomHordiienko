using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ctor.Application.Roles.Queries;
public record GetRolesQuery : IRequest<List<RoleDto>>
{
    public GetRolesQuery() { }
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRolesQueryHandler> _logger;

    public GetRolesQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetRolesQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var filteredRoles = await _context.Roles.GetFilteredWithTotalSum(r => r.Type != UserRoles.Admin, 0, 0, "Id");
        var smth = filteredRoles.entities;

        return _mapper.Map<List<RoleDto>>(smth);

    }
}
