using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Phases.Commands.AddPhase;
public class AddPhaseCommandValidator : AbstractValidator<AddPhaseCommand>
{
    public AddPhaseCommandValidator()
    {
        RuleFor(phase => phase.PhaseName).NotEmpty();
        RuleFor(phase => phase.StartDate).LessThan(phase => phase.EndDate);
    }
}
