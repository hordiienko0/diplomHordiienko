using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Users.Queries;

public class UserByCompanyIdResponseDto : IMapFrom<User>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserByCompanyIdResponseDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
    }
    
}