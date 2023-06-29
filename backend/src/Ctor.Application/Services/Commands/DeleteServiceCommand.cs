using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Services.Commands;
public record DeleteServiceCommand: IRequest<long>
{
    public long Id { get; set; }
}

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, long>
{
    private readonly IApplicationDbContext _context;
    public DeleteServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<long> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _context.Vendors.DeleteById(request.Id);
        await _context.SaveChangesAsync();
        if (!isDeleted)
        {
            throw new NotFoundException("Such service does not exist.");
        }
        return request.Id;

    }
}
