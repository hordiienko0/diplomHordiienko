using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Commands;

public class ProjectDetailedRequestDto : IMapFrom<Project>
{
    public long Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public DateTime StartTime { get; set; } = default;
    public DateTime EndTime { get; set; } = default;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDetailedRequestDto, Project>()
            .ReverseMap();
    }
}