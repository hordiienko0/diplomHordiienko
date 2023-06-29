using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities.Enums;
using MediatR;

namespace Ctor.Application.Resources.Commands.DeleteRequiredMaterial;

public record DeleteRequiredMaterialCommand(long RequiredMaterialId) : IRequest<Unit>;

public class DeleteRequiredMaterialCommandHandler : IRequestHandler<DeleteRequiredMaterialCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteRequiredMaterialCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteRequiredMaterialCommand request, CancellationToken cancellationToken)
    {
        if (_currentUserService.Role is not (UserRoles.OperationalManager or UserRoles.ProjectManager))
        {
            throw new ForbiddenAccessException();
        }

        var isFound = await _context.RequiredMaterials
            .AnyAsync(m => m.Id == request.RequiredMaterialId && m.Material.CompanyId == _currentUserService.CompanyId);

        if (!isFound)
        {
            throw new NotFoundException();
        }

        await _context.RequiredMaterials.DeleteById(request.RequiredMaterialId);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}