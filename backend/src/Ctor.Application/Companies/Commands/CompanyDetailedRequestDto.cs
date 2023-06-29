using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Commands;

public class CompanyDetailedRequestDto : IMapFrom<Company>
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CompanyIdResponseDto, Company>()
            .ReverseMap();
    }
}