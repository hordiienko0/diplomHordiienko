using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Projects.Queries.GetProjectsQuery;
public class GetProjectsOverviewQueryValidator : AbstractValidator<GetProjectsOverviewQuery>
{
    public GetProjectsOverviewQueryValidator()
    {
        RuleFor(p => p.QueryModel.Page).GreaterThanOrEqualTo(1);
        RuleFor(p => p.QueryModel.Count).GreaterThan(0);
        RuleFor(p => p.QueryModel.Status).NotNull();
        RuleFor(p => p.QueryModel.Sort).NotEmpty().NotNull();
    }
}
