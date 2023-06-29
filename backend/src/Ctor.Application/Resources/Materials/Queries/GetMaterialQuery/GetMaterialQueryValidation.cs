using Ctor.Application.Resources.Queries.GetMaterialQuery;
using FluentValidation;

namespace Ctor.Application.Resources.Materials.Queries.GetMaterialQuery;
public class GetMaterialQueryValidation : AbstractValidator<GetMaterialsQuery>
{
    public GetMaterialQueryValidation()
    {
        RuleFor(p => p.QueryModel.Sort).NotEmpty().NotNull();
    }
}
