using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Users.Queries.GetCompanyUsers;

public class GetCompanyUsersDto
{
    public ICollection<GetCompanyUsersUserDto> Users { get; set; } = Array.Empty<GetCompanyUsersUserDto>();
}

public class GetCompanyUsersUserDto : IMapFrom<User>
{
    public long Id { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Role { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string? PhoneNumber { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, GetCompanyUsersUserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserEmail))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
    }
}