using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Projects.Queries;

public record GetProjectPhotosByProjectIdQuery(long ProjectId): IRequest<List<GetProjectPhotoByProjectIdResponseDto>>;

public class GetProjectPhotosByProjectIdQueryHandler : IRequestHandler<GetProjectPhotosByProjectIdQuery, List<GetProjectPhotoByProjectIdResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectPhotosByProjectIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetProjectPhotoByProjectIdResponseDto>> Handle(GetProjectPhotosByProjectIdQuery request,
        CancellationToken cancellationToken)
    {
        var projectPhotos = await _context.ProjectsPhotos.Get(f => f.ProjectId == request.ProjectId);
        if (!projectPhotos.Any()) throw new NotFoundException("No photos for project was found");

        var response = _mapper.Map<List<GetProjectPhotoByProjectIdResponseDto>>(projectPhotos);
        return response;
    }
}