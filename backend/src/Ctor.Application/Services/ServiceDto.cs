using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Services;
public class ServiceDto : IMapFrom<Vendor>
{
    public long Id { get; set; }
    public string[] Types { get; set; }
    public string Company { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
    public long CompanyId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vendor, ServiceDto>()
           .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.VendorTypes.Select(vt => vt.Name)))
           .ForMember(dest=>dest.Company, opt=>opt.MapFrom(src=>src.VendorName));

        profile.CreateMap<ServiceDto, Vendor>()
            .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Company));
    }
}