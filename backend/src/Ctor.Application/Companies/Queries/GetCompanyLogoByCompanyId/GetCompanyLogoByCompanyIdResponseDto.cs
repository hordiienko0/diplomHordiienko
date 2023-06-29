using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Queries.GetCompanyLogoByCompanyId;

public class GetCompanyLogoByCompanyIdResponseDto: IMapFrom<CompanyLogo>
{
    public long Id { get; set; }
    public string Link { get; set; } = string.Empty;

}