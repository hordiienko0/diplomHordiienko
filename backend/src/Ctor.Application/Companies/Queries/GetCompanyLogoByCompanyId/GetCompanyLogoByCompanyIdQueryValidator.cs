using FluentValidation;

namespace Ctor.Application.Companies.Queries.GetCompanyLogoByCompanyId;

public class GetCompanyLogoByCompanyIdQueryValidator: AbstractValidator<GetCompanyLogoByCompanyIdQuery>
{
    public GetCompanyLogoByCompanyIdQueryValidator()
    {
        RuleFor(e => e.CompanyId).GreaterThan(0);
    }
}