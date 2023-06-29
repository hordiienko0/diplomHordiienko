using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Resources.Queries.GetMaterialQuery;
public class GetMaterialsQueryDto : IMapFrom<Material>
{
    public long? Id { get; set; }
    public string MaterialType { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Measurement { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public decimal TotalAmount => Price * Amount;
    public long? CompanyId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Material, GetMaterialsQueryDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd.MM.yyyy")))
            .ForMember(dest => dest.MaterialType, opt => opt.MapFrom(src => src.MaterialType.Name))
            .ForMember(dest => dest.Measurement, opt => opt.MapFrom(src => src.Measurement.Name));
    }
}