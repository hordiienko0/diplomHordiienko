using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Buildings.Queries.GetBuildingsListByProjectId;

public class BuildingBlockDto : IMapFrom<BuildingBlock>
{
    public long Id { get; set; }
    public string BuildingBlockName { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public long BuildingId { get; set; }
}