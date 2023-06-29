using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Services.Commands;
public record DeleteRequiredServiceCommand(long BuildingId, long ServiceId):IRequest<Unit>;

public class DeleteRequiredServiceCommandHandler : IRequestHandler<DeleteRequiredServiceCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteRequiredServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteRequiredServiceCommand request, CancellationToken cancellationToken)
    {
        var service = (await _context.RequiredServices.Get(s => s.VendorId == request.ServiceId && s.BuildingId == request.BuildingId)).First();
        _context.RequiredServices.Delete(service);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}


