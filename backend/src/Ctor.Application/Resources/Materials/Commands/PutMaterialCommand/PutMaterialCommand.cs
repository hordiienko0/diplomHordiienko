using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Resources.Materials.Commands.PutMaterialCommand;
public record PutMaterialCommand : IRequest<PutMaterialCommandDto>
{
    public long Id { get; set; }
    public string MaterialType { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Measurement { get; set; } = string.Empty;
}

public class UpdateMaterialCommandHandler : IRequestHandler<PutMaterialCommand, PutMaterialCommandDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PutMaterialCommandDto> Handle(PutMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _context.Materials.GetById(request.Id, cancellationToken);

        if (material == null) throw new NotFoundException("Such material was not found");

        var materialType = await _context.MaterialType.SingleOrDefault(x => x.Name == request.MaterialType);
        if (materialType == null)
            materialType = new MaterialType() { Name = request.MaterialType };

        var measurement = await _context.Measurements.FirstOrDefault(x => x.Name == request.Measurement);
        if(measurement == null)
            if (material == null) throw new NotFoundException("Such measurement was not found");


        material.MaterialType = materialType;
        material.CompanyName = request.CompanyName;
        material.CompanyAddress = request.CompanyAddress;
        material.Measurement = measurement;
        material.Amount = request.Amount;
        material.Price = request.Price;
        material.Date = DateTime.UtcNow;

        _context.Materials.Update(material);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PutMaterialCommandDto>(material);
    }
}