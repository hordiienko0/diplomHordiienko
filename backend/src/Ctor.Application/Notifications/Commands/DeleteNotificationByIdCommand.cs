using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Notifications.Commands;
public record DeleteNotificationByIdCommand(long Id) : IRequest<Unit>;

public class DeleteBuildingCommandHandler : IRequestHandler<DeleteNotificationByIdCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteBuildingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteNotificationByIdCommand request, CancellationToken cancellationToken)
    {
        bool result = await _context.Notifications.DeleteById(request.Id);
        if (!result)
        {
            throw new NotFoundException();
        }
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}

