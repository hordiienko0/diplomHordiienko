using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Queries.GetCompaniesOverview;
public class CompanyOverviewDto : IMapFrom<Company>
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string JoinDate { get; set; } = string.Empty;
    
    public void Mapping(Profile profile) 
    {
        profile.CreateMap<Company, CompanyOverviewDto>()
            .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.JoinDate.ToString("dd.MM.yyyy")))
            .ForMember(dest=>dest.Image, opt => opt.MapFrom(src => src.CompanyLogo == null ? "" : src.CompanyLogo.Link));
    }

}
