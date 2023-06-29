using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Commands.UpdateCompanyProfile;
public class CompanyProfileUpdatedDto : IMapFrom<Company>
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string JoinDate { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Company, CompanyProfileUpdatedDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Country}, {src.City}, {src.Address}"));
    }
}
