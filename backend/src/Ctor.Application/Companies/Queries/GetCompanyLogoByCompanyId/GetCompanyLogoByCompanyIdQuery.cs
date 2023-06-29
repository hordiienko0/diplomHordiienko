using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Companies.Queries.GetCompanyLogoByCompanyId;

public record GetCompanyLogoByCompanyIdQuery(long CompanyId) : IRequest<GetCompanyLogoByCompanyIdResponseDto>;

public class
    GetCompanyLogoByCompanyIdQueryHandler : IRequestHandler<GetCompanyLogoByCompanyIdQuery,
        GetCompanyLogoByCompanyIdResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileManipulatorService _fileManipulatorService;

    public async Task<GetCompanyLogoByCompanyIdResponseDto> Handle(GetCompanyLogoByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        var companyLogo = await _context.CompanyLogos.SingleOrDefault(x=>x.CompanyId == request.CompanyId);
        if (companyLogo == null) throw new NotFoundException("No logo for company was found");

        var response = _mapper.Map<CompanyLogo, GetCompanyLogoByCompanyIdResponseDto>(companyLogo);
        return response;
    }

    
    public GetCompanyLogoByCompanyIdQueryHandler(IApplicationDbContext context, IMapper mapper,
        IFileManipulatorService fileManipulatorService)
    {
        _context = context;
        _mapper = mapper;
        _fileManipulatorService = fileManipulatorService;
    }
}