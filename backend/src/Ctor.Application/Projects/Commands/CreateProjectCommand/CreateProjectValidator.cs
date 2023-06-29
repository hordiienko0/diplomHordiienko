using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Projects.Commands.CreateProjectCommand;
public class CreateProjectValidator : AbstractValidator<CreateProjectDTO>
{
    public CreateProjectValidator()
    {
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty().LessThan(x=>x.EndDate);
        RuleFor(c => c.EndDate).NotEmpty().GreaterThan(x=>x.StartDate);
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.ProjectId).NotEmpty().GreaterThan(10000000);     
        
    }
}
