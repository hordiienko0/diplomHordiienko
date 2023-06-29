using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Companies.Commands;

public record PutCompanyDetailedCommand
    (CompanyDetailedRequestDto CompanyRequest) : IRequest<CompanyIdResponseDto>;

public class PutCompanyDetailedCommandHandler : IRequestHandler<PutCompanyDetailedCommand, CompanyIdResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public async Task<CompanyIdResponseDto> Handle(PutCompanyDetailedCommand request,
        CancellationToken cancellationToken)
    {
        var comp = await _context.Companies.GetById(request.CompanyRequest.Id);
        if (comp == null) throw new NotFoundException("No such company was found");

        comp = _mapper.Map(request.CompanyRequest, comp);

        await _context.SaveChangesAsync(cancellationToken);

        var companyResponse = _mapper.Map<Company, CompanyIdResponseDto>(comp);
        return companyResponse;
    }

    public PutCompanyDetailedCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}