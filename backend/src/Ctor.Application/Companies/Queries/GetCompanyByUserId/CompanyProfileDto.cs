using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Queries.GetCompanyByUserId;
public class CompanyProfileDto : IMapFrom<Company>
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string JoinDate { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Company, CompanyProfileDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Country}, {src.City}, {src.Address}"));
    }
}
