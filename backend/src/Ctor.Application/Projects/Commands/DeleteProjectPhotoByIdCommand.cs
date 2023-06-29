using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Projects.Commands;

public record DeleteProjectPhotoByIdCommand(long ProjectId, long ProjectPhotoId) : IRequest<DeleteProjectPhotoByIdResponseDto>;

public class DeleteProjectPhotoByIdCommandHandler : IRequestHandler<DeleteProjectPhotoByIdCommand, DeleteProjectPhotoByIdResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileManipulatorService _fileManipulatorService;

    public async Task<DeleteProjectPhotoByIdResponseDto> Handle(DeleteProjectPhotoByIdCommand request, CancellationToken cancellationToken)
    {
        var photo = await _context.ProjectsPhotos.SingleOrDefault(f =>
            f.ProjectId == request.ProjectId && f.Id == request.ProjectPhotoId);
        if (photo == null) throw new NotFoundException("Such project photo was not found");

        await _fileManipulatorService.Delete(photo.Path);
        _context.ProjectsPhotos.Delete(photo);
        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<DeleteProjectPhotoByIdResponseDto>(photo);
        return result;
    }

    public DeleteProjectPhotoByIdCommandHandler(IApplicationDbContext context, IMapper mapper,
        IFileManipulatorService fileManipulatorService)
    {
        _context = context;
        _mapper = mapper;
        _fileManipulatorService = fileManipulatorService;
    }
}