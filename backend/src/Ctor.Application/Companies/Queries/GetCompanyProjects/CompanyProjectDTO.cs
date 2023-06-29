using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Application.Companies.Queries.GetCompanyByUserId;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Companies.Queries.GetCompanyProjects;
public class CompanyProjectDTO : IMapFrom<Project>
{
    public long Id { get; set; }
    public string ProjectName { get; set; }
    public string Address { get; set; }
    public string Description { get; set; } = String.Empty;
    public string Status { get; set; }
    public string? ImageUrl { get; set; }
    public int? ImageAmount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, CompanyProjectDTO>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProjectPhotos.Count() > 0 ? src.ProjectPhotos.First().Link : null))
            .ForMember(dest => dest.ImageAmount, opt => opt.MapFrom(src => src.ProjectPhotos.Count()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Country}, {src.City}, {src.Address}"));
    }
}
