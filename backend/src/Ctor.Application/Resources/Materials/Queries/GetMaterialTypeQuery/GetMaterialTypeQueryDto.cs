using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Resources.Materials.Queries.GetMaterialTypeQuery;
public class GetMaterialTypeQueryDto : IMapFrom<MaterialType>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MaterialType, GetMaterialTypeQueryDto>();
    }
}