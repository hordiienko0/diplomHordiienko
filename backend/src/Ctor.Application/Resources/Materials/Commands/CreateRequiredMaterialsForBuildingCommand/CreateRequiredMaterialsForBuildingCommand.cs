using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Resources.Materials.Commands.CreateRequiredMaterialsForBuildingCommand;

public record CreateRequiredMaterialsForBuildingCommand(CreateRequiredMaterialsForBuildingDto[] materials) : IRequest;

public class CreateRequiredMaterialsForBuildingHandler : IRequestHandler<CreateRequiredMaterialsForBuildingCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly INotificationService _notifService;
    private ICurrentUserService _currentUserService;
    public CreateRequiredMaterialsForBuildingHandler(IApplicationDbContext context, INotificationService notifService, ICurrentUserService currentUserService)
    {
        this._context = context;
        this._notifService = notifService;
        this._currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CreateRequiredMaterialsForBuildingCommand request, CancellationToken cancellationToken)
    {
        foreach (var material in request.materials)
        {
            if (!_currentUserService.CompanyId.HasValue) throw new ForbiddenAccessException();
            var required = await _context.RequiredMaterials.Get(el => el.Building.Project.CompanyId == _currentUserService.CompanyId);
            var sum = required.Where(el => el.MaterialId == material.Id).Select(el => el.Amount).Sum();
            var maxAlowed = await _context.Materials.FirstOrDefault(el => el.Id == material.Id, cancellationToken);
            if (material.Amount + sum <= maxAlowed.Amount)
            {
                RequiredMaterial newMaterial = new RequiredMaterial()
                {
                    Amount = material.Amount,
                    BuildingId = material.BuildingId,
                    MaterialId = material.Id,
                };
                await this._context.RequiredMaterials.AddRangeAsync(newMaterial);
            }
        }
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}

