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
public record AddServiceCommand : IRequest<ServiceDto>
{
    public string[] Types { get; set; }
    public string Company { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
}

public class AddUserQueryHandler : IRequestHandler<AddServiceCommand, ServiceDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;
    public AddUserQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    public async Task<ServiceDto> Handle(AddServiceCommand request, CancellationToken cancellationToken)
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

        Vendor vendor = new Vendor
        {
            VendorName = request.Company,
            Email = request.Email,
            Phone = request.Phone,
            Website = request.Website,
            VendorTypes = types,
            CompanyId = (long)_currentUser.CompanyId,
        };
        await _context.Vendors.Insert(vendor);
        await _context.SaveChangesAsync();

        var service = await _context.Vendors.FirstOrDefault(s =>
        s.VendorName == request.Company &&
        s.Email == request.Email &&
        s.Website == request.Website &&
        s.Phone == request.Phone);

        return _mapper.Map<ServiceDto>(service);

    }
}
