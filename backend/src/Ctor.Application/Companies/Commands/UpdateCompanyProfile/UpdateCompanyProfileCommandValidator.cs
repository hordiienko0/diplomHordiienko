using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Companies.Commands.UpdateCompanyProfile;
public class UpdateCompanyProfileCommandValidator : AbstractValidator<UpdateCompanyProfileCommand>
{
    public UpdateCompanyProfileCommandValidator()
    {
        RuleFor(c => c.Address)
            .Matches(@"[\w\s-]+[,][ ][\w\s-]+[,][ ].+")
            .WithMessage("Address should be in format 'Germany, Berlin, Green Str 12'");
        RuleFor(c => c.Email)
            .EmailAddress();
    }
}
