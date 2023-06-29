using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Projects.Queries.GetProjectsQuery;
public class GetProjectsDetailedQueryValidator : AbstractValidator<GetProjectDetailedQuery>
{
    public GetProjectsDetailedQueryValidator()
    {
        RuleFor(p => p.id).GreaterThan(0);
    }
}
