using AutoMapper;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.ProjectDocuments.Commands.PutProjectDocument;

public record PutProjectDocumentCommand(PutProjectDocumentRequestDto RequestDto) : IRequest<PutProjectDocumentResponseDto>;

public class
    PutProjectDocumentCommandHandler : IRequestHandler<PutProjectDocumentCommand, PutProjectDocumentResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public PutProjectDocumentCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PutProjectDocumentResponseDto> Handle(PutProjectDocumentCommand request, CancellationToken cancellationToken)
    {
        var projectDocument = await _context.ProjectDocuments.GetById(request.RequestDto.Id, cancellationToken);
        projectDocument.Document = await _context.Documents.FirstOrDefault(x=>x.Id==projectDocument.DocumentId);

        projectDocument = _mapper.Map(request.RequestDto, projectDocument);
        _context.ProjectDocuments.Update(projectDocument);
        await _context.SaveChangesAsync(cancellationToken);
        var responseWithProjectId =
            await _context.ProjectDocuments.GetById<PutProjectDocumentResponseDto>(projectDocument.Id,
                cancellationToken);
        return responseWithProjectId;
    }
}