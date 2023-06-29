using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Services.Queries;
public record GetTypesQuery:IRequest<IEnumerable<string>>
{

}
public class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, IEnumerable<string>>
{
    private readonly IApplicationDbContext _context;
    public GetTypesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<string>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _context.VendorTypes.GetAll();
        return types.Select(t => t.Name);
    }
}
