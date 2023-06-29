using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Companies.Commands.DeleteCompanyLogo;

public record DeleteCompanyLogoCommand(long CompanyId): IRequest<DeleteCompanyLogoResponseDto>;

public class DeleteCompanyLogoCommandHandler : IRequestHandler<DeleteCompanyLogoCommand, DeleteCompanyLogoResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileManipulatorService _fileManipulatorService;

    public async Task<DeleteCompanyLogoResponseDto> Handle(DeleteCompanyLogoCommand request, CancellationToken cancellationToken)
    {
        var logo = await _context.CompanyLogos.SingleOrDefault(x => x.CompanyId == request.CompanyId);
        if (logo == null) throw new NotFoundException("No such company logo was found");

        await _fileManipulatorService.Delete(logo.Path);
        _context.CompanyLogos.Delete(logo);
        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<DeleteCompanyLogoResponseDto>(logo);
        return result;
    }

    public DeleteCompanyLogoCommandHandler(IApplicationDbContext context, IMapper mapper, IFileManipulatorService fileManipulatorService)
    {
        _context = context;
        _mapper = mapper;
        _fileManipulatorService = fileManipulatorService;
    }
}