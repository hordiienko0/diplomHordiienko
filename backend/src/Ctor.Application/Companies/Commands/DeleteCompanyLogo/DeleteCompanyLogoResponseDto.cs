using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Commands.DeleteCompanyLogo;

public class DeleteCompanyLogoResponseDto: IMapFrom<CompanyLogo>
{
    public long Id { get; set; }
    public long CompanyId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CompanyLogo, DeleteCompanyLogoResponseDto>().ReverseMap();
    }
}