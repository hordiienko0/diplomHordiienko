using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Companies.Commands.UpdateCompanyProfile;
public record UpdateCompanyProfileCommand : IRequest<CompanyProfileUpdatedDto>
{
    public long Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
}

public class UpdateCompanyProfileCommandHandler : IRequestHandler<UpdateCompanyProfileCommand, CompanyProfileUpdatedDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IAddressParsingService _addressParsing;
    private readonly IMapper _mapper;

    public UpdateCompanyProfileCommandHandler(IApplicationDbContext context, IAddressParsingService addressParsing, IMapper mapper)
    {
        _context = context;
        _addressParsing = addressParsing;
        _mapper = mapper;
    }

    public async Task<CompanyProfileUpdatedDto> Handle(UpdateCompanyProfileCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies.FirstOrDefault(x=>x.Id==request.Id, cancellationToken);
        if (company == null)
        {
            throw new NotFoundException();
        }
        var parsedAddressModel = _addressParsing.ParseAddress(request.Address);
        company.Address = parsedAddressModel.Address;
        company.City = parsedAddressModel.City;
        company.Country = parsedAddressModel.Country;
        company.Email = request.Email;
        company.Website = request.Website;

        _context.Companies.Update(company);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompanyProfileUpdatedDto>(company);
    }
}