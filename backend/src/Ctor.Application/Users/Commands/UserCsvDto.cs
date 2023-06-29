using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;
using FluentValidation;

namespace Ctor.Application.Users.Commands;
public class UserCsvDto : IMapFrom<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserEmail { get; set; }
    public string RoleName { get; set; }

}
public class UserCsvDtoValidator : AbstractValidator<UserCsvDto>
{
    public UserCsvDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.UserEmail).NotEmpty().Must(e => e.Contains('@'));
        RuleFor(x => x.RoleName).NotEmpty();
    }
}