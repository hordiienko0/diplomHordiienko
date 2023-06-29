using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Services.Commands;
public class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
{
    public AddServiceCommandValidator()
    {
        RuleFor(s => s.Email)
          .EmailAddress()
          .WithMessage("Wrong email address")
          .NotEmpty();
        RuleFor(s => s.Phone)
            .NotEmpty()
            .Matches("[- +()0-9]+")
            .WithMessage("Wrong phone format");
        RuleFor(s => s.Website)
            .NotEmpty()
            .Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:\?#[\]@!\$&'\(\)\*\+,;=.]+$")
            .WithMessage("Wrong website input");
        RuleFor(s => s.Company)
            .NotEmpty();
        RuleFor(s => s.Types)
            .NotEmpty()
            .NotNull();
    }
}
