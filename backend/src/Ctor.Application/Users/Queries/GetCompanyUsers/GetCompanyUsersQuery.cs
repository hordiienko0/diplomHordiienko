using System.Linq.Expressions;
using AutoMapper;
using Ctor.Application.Common.Enums;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Users.Queries.GetCompanyUsers;

public record GetCompanyUsersQuery(string? Filter, string? Sort) : IRequest<GetCompanyUsersDto>;

public class GetCompanyUsersQueryHandler : IRequestHandler<GetCompanyUsersQuery, GetCompanyUsersDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetCompanyUsersQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<GetCompanyUsersDto> Handle(GetCompanyUsersQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter?.Trim();
        var sort = request.Sort?.Trim() ?? "role";
        var companyId = _currentUserService.CompanyId!.Value;

        Expression<Func<User, bool>> filterPredicate = string.IsNullOrWhiteSpace(filter)
            ? user => user.CompanyId == companyId
            : user => user.CompanyId == companyId
                      && (user.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase)
                          || user.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase));

        var users = await _context.Users.GetOrdered<GetCompanyUsersUserDto>(sort, Order.ASC, filterPredicate);

        return new GetCompanyUsersDto { Users = users, };
    }
}