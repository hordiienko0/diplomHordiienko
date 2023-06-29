using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Companies.Queries.GetCompaniesOverview;
internal class GetCompaniesOverviewQueryValidator : AbstractValidator<GetCompaniesOverviewQuery>
{
    public GetCompaniesOverviewQueryValidator()
    {
        RuleFor(v => v.Sort)
            .Must(filter => filter == "name" || filter == "joinDate");
    }
}
