using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Companies.Commands;

public record PutProjectDetailedCommand
    (ProjectDetailedRequestDto Project) : IRequest<IdResposeDTO>;

public class PutProjectDetailedCommandHandler : IRequestHandler<PutProjectDetailedCommand, IdResposeDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PutProjectDetailedCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IdResposeDTO> Handle(PutProjectDetailedCommand request,
        CancellationToken cancellationToken)
    {

        var project = await _context.Projects.FirstOrDefault(p => p.Id == request.Project.Id);

        project = _mapper.Map(request.Project, project);

        await _context.SaveChangesAsync(cancellationToken);

        return new IdResposeDTO { Id = project.Id };
    }


}