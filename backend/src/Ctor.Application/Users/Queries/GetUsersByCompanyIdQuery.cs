using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Companies.Queries;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Users.Queries;

public record GetUsersByCompanyIdQuery(long CompanyId) : IRequest<List<UserByCompanyIdResponseDto>>;

public class
    GetUserByCompanyIdQueryHandler : IRequestHandler<GetUsersByCompanyIdQuery, List<UserByCompanyIdResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserByCompanyIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<List<UserByCompanyIdResponseDto>> Handle(GetUsersByCompanyIdQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _context.Users.Get(f => f.CompanyId == request.CompanyId);
        if (!users.Any()) throw new NotFoundException("No users with such companyId was found");
        foreach (var user in users)
        {
            user.Role = await _context.Roles.SingleOrDefault(f => f.Id == user.RoleId);
        }
        var result = _mapper.Map<List<UserByCompanyIdResponseDto>>(users);
        return result;
    }
}