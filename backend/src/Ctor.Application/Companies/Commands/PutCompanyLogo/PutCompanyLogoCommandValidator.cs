using FluentValidation;

namespace Ctor.Application.Companies.Commands;

public class PutCompanyLogoCommandValidator : AbstractValidator<PutCompanyLogoCommand>
{
    public PutCompanyLogoCommandValidator()
    {
        RuleFor(e => e.CompanyId).GreaterThan(0);
        RuleFor(e => e.Data).NotEmpty();
        RuleFor(e => e.FileType).NotEmpty();
    }
}