using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.Buildings.Commands.AddBuilding;
public class AddBuildingCommandValidator : AbstractValidator<AddBuildingCommand>
{
    public AddBuildingCommandValidator()
    {
        RuleFor(c => c.BuildingName).NotEmpty();
        RuleFor(c => c.ProjectId).GreaterThan(0).NotEmpty();
    }
}
