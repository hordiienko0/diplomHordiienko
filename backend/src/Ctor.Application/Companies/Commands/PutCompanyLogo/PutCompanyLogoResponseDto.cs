using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Companies.Commands;

public class PutCompanyLogoResponseDto : IMapFrom<CompanyLogo>
{
    public long Id { get; set; }
    public string Link { get; set; } = string.Empty;
}