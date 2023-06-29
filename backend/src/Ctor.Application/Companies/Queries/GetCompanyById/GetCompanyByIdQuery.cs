using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Companies.Queries.GetCompanyById;

public record GetCompanyByIdQuery(long Id) : IRequest<CompanyDetailedResponseDto>;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDetailedResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyDetailedResponseDto> Handle(GetCompanyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var company = await _context.Companies.GetById<CompanyDetailedResponseDto>(request.Id, cancellationToken);
        if (company == null) throw new NotFoundException("No such company was found");
        return company;
    }
}