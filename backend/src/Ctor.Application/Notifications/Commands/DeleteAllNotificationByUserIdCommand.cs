using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using MediatR;

namespace Ctor.Application.Notifications.Commands;


public record DeleteAllNotificationByUserIdCommand(long Id) : IRequest<Unit>;

public class DeleteAllNotificationByUserIdCommandHandler : IRequestHandler<DeleteAllNotificationByUserIdCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteAllNotificationByUserIdCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAllNotificationByUserIdCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Notifications.Get(el => el.UserId == request.Id);
        if (result == null)
        {
            throw new NotFoundException();
        }
        else
        {
            foreach(Notification noti in result) await _context.Notifications.DeleteById(noti.Id);
        }
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}
