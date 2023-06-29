using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;
using MediatR;

namespace Ctor.Application.Companies.Commands;

public record PutCompanyLogoCommand(byte[] Data, long CompanyId, string FileType) : IRequest<PutCompanyLogoResponseDto>;

public class PutCompanyLogoCommandHandler : IRequestHandler<PutCompanyLogoCommand, PutCompanyLogoResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileManipulatorService _fileManipulatorService;

    public async Task<PutCompanyLogoResponseDto> Handle(PutCompanyLogoCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies.GetById(request.CompanyId, cancellationToken);
        if (company == null) throw new NotFoundException("No such company was found");

        var companyLogo = await _context.CompanyLogos.SingleOrDefault(f => f.CompanyId == request.CompanyId);
        if (companyLogo != null)
        {
            await _fileManipulatorService.Delete(companyLogo.Path);
            await _context.CompanyLogos.DeleteById(companyLogo.Id);
        }

        var path = $"companyLogos\\{Guid.NewGuid()}{request.FileType}";

        var link = await _fileManipulatorService.Save(request.Data, path);

        if (link == null)
            throw new IOException();

        companyLogo = new CompanyLogo()
        {
            Path = path,
            CompanyId = request.CompanyId,
            Link = link,
            Type = FileProviderType.Local
        };

        await _context.CompanyLogos.AddRangeAsync(companyLogo);
        await _context.SaveChangesAsync(cancellationToken);
        company.CompanyLogoId = companyLogo.Id;
        await _context.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<CompanyLogo, PutCompanyLogoResponseDto>(companyLogo);
        return response;
    }


    public PutCompanyLogoCommandHandler(IApplicationDbContext context, IMapper mapper,
        IFileManipulatorService fileManipulatorService)
    {
        _context = context;
        _mapper = mapper;
        _fileManipulatorService = fileManipulatorService;
    }
}
