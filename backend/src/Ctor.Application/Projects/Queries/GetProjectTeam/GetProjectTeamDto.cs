using AutoMapper;
using Ctor.Application.Common.Mapping;

namespace Ctor.Application.Projects.Queries.GetProjectTeam;

public class GetProjectTeamDto : IMapFrom<Domain.Entities.Project>
{
    public ICollection<GetProjectTeamUserDto> Users { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Project, GetProjectTeamDto>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Assignees));
    }
}

public class GetProjectTeamUserDto : IMapFrom<Domain.Entities.User>
{
    public long Id { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Role { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string? PhoneNumber { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Assignee, GetProjectTeamUserDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.UserEmail))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
    }
}