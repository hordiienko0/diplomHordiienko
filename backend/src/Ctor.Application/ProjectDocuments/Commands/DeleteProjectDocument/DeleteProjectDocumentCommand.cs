using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.ProjectDocuments.Commands.DeleteProjectDocument;

public record DeleteProjectDocumentCommand
    (long ProjectDocumentId) : IRequest<DeleteProjectDocumentResponseDto>;

public class
    DeleteProjectDocumentCommandHandler : IRequestHandler<DeleteProjectDocumentCommand,
        DeleteProjectDocumentResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IFileManipulatorService _fileManipulatorService;

    public DeleteProjectDocumentCommandHandler(IMapper mapper, IApplicationDbContext context,
        IFileManipulatorService fileManipulatorService)
    {
        _mapper = mapper;
        _context = context;
        _fileManipulatorService = fileManipulatorService;
    }

    public async Task<DeleteProjectDocumentResponseDto> Handle(DeleteProjectDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var projectDocumentWithPath =
            await _context.ProjectDocuments.FirstOrDefault<DeleteProjectDocumentPathDto>(
                exp =>  exp.Id == request.ProjectDocumentId, cancellationToken);

        await _fileManipulatorService.Delete(projectDocumentWithPath.Path);

        // delete temporary fix
        await _context.Documents.DeleteById(projectDocumentWithPath.DocumentId);

        await _context.ProjectDocuments.DeleteById(projectDocumentWithPath.Id);
        await _context.SaveChangesAsync(cancellationToken);

        var response =
            _mapper.Map<DeleteProjectDocumentResponseDto>(projectDocumentWithPath);
        return response;
    }
}