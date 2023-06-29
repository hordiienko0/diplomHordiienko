using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Resources.Materials.Commands.DeleteMaterialCommand;
public record DeleteMaterialByIdCommand(long Id) : IRequest<DeleteMaterialByIdCommandDto>;

public class DeleteMaterialByIdCommandHandler : IRequestHandler<DeleteMaterialByIdCommand, DeleteMaterialByIdCommandDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteMaterialByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DeleteMaterialByIdCommandDto> Handle(DeleteMaterialByIdCommand request, CancellationToken cancellationToken)
    {
        var material = await _context.Materials.GetById(request.Id);
        if (material == null) throw new NotFoundException("Such material was not found");

        _context.Materials.Delete(material);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<DeleteMaterialByIdCommandDto>(material);
    }
}
