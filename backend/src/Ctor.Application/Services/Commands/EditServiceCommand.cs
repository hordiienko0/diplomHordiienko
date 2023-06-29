using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Services.Commands;
public record EditServiceCommand: IRequest<ServiceDto>
{
    public long Id { get; set; }
    public string[] Types { get; set; }
    public string Company { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
}
public class EditServiceCommandHandler : IRequestHandler<EditServiceCommand, ServiceDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public EditServiceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceDto> Handle(EditServiceCommand request, CancellationToken cancellationToken)
    {
        var allTypes = await _context.VendorTypes.GetAll();
        var newTypeNames = request.Types.Except(allTypes.Select(t => t.Name));
        var newTypes = newTypeNames.Select(t => new VendorType
        {
            Name = t
        });
        await _context.VendorTypes.AddRangeAsync(newTypes);
        await _context.SaveChangesAsync();

        var types = new List<VendorType>(); 
        foreach (var typeName in request.Types)
        {
            var type = await _context.VendorTypes.FirstOrDefault(vt => vt.Name == typeName);
            types.Add(type);
        }
        var vendor = await _context.Vendors.GetByIdWithVendorTypes(request.Id);
        vendor.VendorName = request.Company;
        vendor.Phone = request.Phone;
        vendor.Email = request.Email;
        vendor.Website = request.Website;
        var typesToAdd = types.Except(vendor.VendorTypes);
        var typesToRemove = vendor.VendorTypes.Except(types);
        foreach(var typeToRemove in typesToRemove)
        {
            vendor.VendorTypes.Remove(typeToRemove);
        }
        foreach(var type in typesToAdd)
        {
            vendor.VendorTypes.Add(type);
        }

         _context.Vendors.Update(vendor);
        await _context.SaveChangesAsync();

        return _mapper.Map<ServiceDto>(vendor);

    }
}
