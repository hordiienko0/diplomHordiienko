using FluentValidation;

namespace Ctor.Application.Users.Queries;

public class UsersByCompanyIdQueryValidator : AbstractValidator<GetUsersByCompanyIdQuery>
{
    public UsersByCompanyIdQueryValidator()
    {
        RuleFor(v => v.CompanyId).GreaterThan(0);
    }
}