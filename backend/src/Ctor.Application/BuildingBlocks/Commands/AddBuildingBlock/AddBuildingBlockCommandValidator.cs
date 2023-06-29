using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ctor.Application.BuildingBlocks.Commands.AddBuildingBlock;
public class AddBuildingBlockCommandValidator : AbstractValidator<AddBuildingBlockCommand>
{
    public AddBuildingBlockCommandValidator()
    {
        RuleFor(c => c.BuildingBlockName).NotEmpty();
    }
}
