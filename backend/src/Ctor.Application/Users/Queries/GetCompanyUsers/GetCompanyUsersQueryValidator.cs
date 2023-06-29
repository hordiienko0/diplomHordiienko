using FluentValidation;

namespace Ctor.Application.Users.Queries.GetCompanyUsers;

internal class GetCompanyUsersQueryValidator : AbstractValidator<GetCompanyUsersQuery>
{
    public GetCompanyUsersQueryValidator()
    {
        RuleFor(v => v.Filter);

        RuleFor(v => v.Sort)
            .Must(sort => string.IsNullOrEmpty(sort) || sort is "role" or "firstName");
    }
}