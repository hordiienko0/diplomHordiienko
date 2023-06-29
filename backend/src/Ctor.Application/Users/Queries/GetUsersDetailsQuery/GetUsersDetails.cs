using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Users.Queries.GetUsersDetailsQuery;
public record GetUsersDetailsQuery(long UserId) : IRequest<UserDetailsDto>;

public class GetUserByCompanyIdQueryHandler : IRequestHandler<GetUsersDetailsQuery, UserDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserByCompanyIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<UserDetailsDto> Handle(GetUsersDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.GetById(request.UserId);
        if (user==null) throw new NotFoundException("No users with such companyId was found");
        
        var result = _mapper.Map<UserDetailsDto>(user);
        return result;
    }
}
