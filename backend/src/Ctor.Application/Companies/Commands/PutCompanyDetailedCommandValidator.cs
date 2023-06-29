using FluentValidation;

namespace Ctor.Application.Companies.Commands;

public class PutCompanyDetailedCommandValidator : AbstractValidator<PutCompanyDetailedCommand>
{
    public PutCompanyDetailedCommandValidator()
    {
        RuleFor(c => c.CompanyRequest.Id).GreaterThan(0);
        RuleFor(c => c.CompanyRequest.CompanyName).NotEmpty();
        RuleFor(c => c.CompanyRequest.Email).EmailAddress();
    }
}