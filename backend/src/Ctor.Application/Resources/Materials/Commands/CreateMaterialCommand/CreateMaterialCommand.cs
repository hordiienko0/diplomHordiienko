using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Resources.Materials.Commands.CreateMaterialCommand;
public class CreateMaterialCommand : IRequest
{
    public CreateMaterialCommand(CreateMaterialCommandDto model)
    {
        Model = model;
    }
    public CreateMaterialCommandDto Model { get; set; }
}

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateMaterialCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;

    }

    public async Task<Unit> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var materialType = await _context.MaterialType.SingleOrDefault(x => x.Name == request.Model.MaterialType);

        if (materialType == null)
            materialType = new MaterialType() { Name = request.Model.MaterialType };

        var measurement = await _context.Measurements.FirstOrDefault(x => x.Name == request.Model.Measurement);

        var materials = new Material()
        {
            MaterialType = materialType,
            CompanyName = request.Model.CompanyName,
            CompanyAddress = request.Model.CompanyAddress,
            Amount = request.Model.Amount,
            Measurement = measurement,
            Price = request.Model.Price,
            Date = DateTime.UtcNow,
            CompanyId = (long)_currentUser.CompanyId,
        };

        await _context.Materials.Insert(materials);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}