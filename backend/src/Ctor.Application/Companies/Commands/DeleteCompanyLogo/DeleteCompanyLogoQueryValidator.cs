using FluentValidation;

namespace Ctor.Application.Companies.Commands.DeleteCompanyLogo;

public class DeleteCompanyLogoQueryValidator : AbstractValidator<DeleteCompanyLogoCommand>
{
    public DeleteCompanyLogoQueryValidator()
    {
        RuleFor(e => e.CompanyId).GreaterThan(0);
    }
}