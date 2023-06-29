using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;
using MediatR;

namespace Ctor.Application.Projects.Commands;

public record PutProjectPhotosCommand
    (byte[][] Data, long ProjectId) : IRequest<List<PutProjectPhotoResponseDto>>;

public class PutProjectPhotoCommandHandler : IRequestHandler<PutProjectPhotosCommand, List<PutProjectPhotoResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileManipulatorService _fileManipulatorService;

    public async Task<List<PutProjectPhotoResponseDto>> Handle(PutProjectPhotosCommand request,
        CancellationToken cancellationToken)
    {

        var project = await _context.Projects.GetById(request.ProjectId, cancellationToken);
        if (project == null) throw new NotFoundException("No such project was found");
        List<ProjectPhoto> projectPhotos = new List<ProjectPhoto>();
        foreach (var file in request.Data)
        {
            var path = Path.Combine("projectPhotos", $"{Guid.NewGuid()}.png");

            var link = await _fileManipulatorService.Save(file, path);

            if (link == null)
                throw new IOException();
            
            var projectPhoto = new ProjectPhoto()
            {
                Path = path, ProjectId = request.ProjectId, Link = link, Type = FileProviderType.Local
            };
            await _context.ProjectsPhotos.AddRangeAsync(projectPhoto);
            await _context.SaveChangesAsync(cancellationToken);
            projectPhotos.Add(projectPhoto);
        }
       

        var result = _mapper.Map<List<PutProjectPhotoResponseDto>>(projectPhotos);
        return result;
    }

    public PutProjectPhotoCommandHandler(IApplicationDbContext context, IMapper mapper,
        IFileManipulatorService fileManipulatorService)
    {
        _context = context;
        _mapper = mapper;
        _fileManipulatorService = fileManipulatorService;
    }
}