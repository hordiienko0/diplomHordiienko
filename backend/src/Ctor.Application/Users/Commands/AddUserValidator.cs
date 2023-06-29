using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using FluentValidation;

namespace Ctor.Application.Users.Commands;
public class AddUserValidator: AbstractValidator<AddUserCommand>
{
    public AddUserValidator()
    {
        RuleFor(x => x.UserEmail)
            .EmailAddress().WithMessage("Wrong emaill adress.");
    }
}
