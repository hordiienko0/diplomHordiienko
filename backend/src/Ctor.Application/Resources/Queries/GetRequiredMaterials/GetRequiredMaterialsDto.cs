using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Resources.Queries.GetRequiredMaterials;

public class GetRequiredMaterialsDto
{
    public ICollection<GetRequiredMaterialDto> RequiredMaterials { get; set; }
}

public class GetRequiredMaterialDto : IMapFrom<RequiredMaterial>
{
    public long Id { get; set; }
    public long Amount { get; set; }
    public string Measurement { get; set; }
    public string CompanyName { get; set; }
    public string CompanyAddress { get; set; }
    public string Type { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RequiredMaterial, GetRequiredMaterialDto>()
            .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Amount))
            .ForMember(x => x.Measurement, opt => opt.MapFrom(x => x.Material.Measurement!.Name))
            .ForMember(x => x.CompanyName, opt => opt.MapFrom(x => x.Material.CompanyName))
            .ForMember(x => x.CompanyAddress, opt => opt.MapFrom(x => x.Material.CompanyAddress))
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Material.MaterialType!.Name));
    }
}