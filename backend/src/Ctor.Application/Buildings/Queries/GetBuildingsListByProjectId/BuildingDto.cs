using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Buildings.Queries.GetBuildingsListByProjectId;
public class BuildingDto : IMapFrom<Building>
{
    public long Id { get; set; }
    public string BuildingName { get; set; } = string.Empty;
    public int BuildingProgress { get; set; }
    public ICollection<BuildingBlockDto> BuildingBlocks { get; set; }
    public long ProjectId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Building, BuildingDto>()
            .ForMember(dest => dest.BuildingProgress, opt => opt.MapFrom(src =>
                src.BuildingBlocks.Count == 0 ?
                0 :
                (int)Math.Round(src.BuildingBlocks.Where(block => block.IsDone).Count() * 100d / src.BuildingBlocks.Count)
            ));
    }
}
