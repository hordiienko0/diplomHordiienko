using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Resources.Materials.Commands.PutMaterialCommand;
public class PutMaterialCommandDto : IMapFrom<Material>
{
    public long? Id { get; set; }
    public string MaterialType { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Measurement { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PutMaterialCommandDto, Material>().ReverseMap()
            .ForMember(dest => dest.MaterialType, opt => opt.MapFrom(src => src.MaterialType.Name))
            .ForMember(dest => dest.Measurement, opt => opt.MapFrom(src => src.Measurement.Name)); ;
    }
}