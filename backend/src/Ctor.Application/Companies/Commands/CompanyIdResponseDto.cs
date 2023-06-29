using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Commands;

public class CompanyIdResponseDto : IMapFrom<Company>
{
    public long Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CompanyDetailedRequestDto, Company>().ReverseMap();
    }
}