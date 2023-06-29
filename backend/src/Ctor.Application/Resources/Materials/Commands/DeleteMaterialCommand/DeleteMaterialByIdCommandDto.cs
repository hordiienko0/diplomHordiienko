using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Resources.Materials.Commands.DeleteMaterialCommand;
public class DeleteMaterialByIdCommandDto : IMapFrom<Material>
{
    public long Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Material, DeleteMaterialByIdCommandDto>().ReverseMap();
    }
}

