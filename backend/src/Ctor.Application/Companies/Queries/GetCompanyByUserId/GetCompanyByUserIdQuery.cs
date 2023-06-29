using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Companies.Queries.GetCompanyByUserId;
public record GetCompanyByUserIdQuery(long UserId) : IRequest<CompanyProfileDto>;

public class GetCompanyByUserIdHandler : IRequestHandler<GetCompanyByUserIdQuery, CompanyProfileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyByUserIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyProfileDto> Handle(GetCompanyByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.GetById(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("User was not found");
        }
        if(user.CompanyId == null)
        {
            throw new NotFoundException("Company for this user doesn't exists");
        }

        var company = await _context.Companies.GetById(user.CompanyId!.Value, cancellationToken);

        return _mapper.Map<CompanyProfileDto>(company);
    }
}
