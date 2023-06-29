using AutoMapper;
using Ctor.Application.Common.Interfaces;
using MediatR;

namespace Ctor.Application.Notifications.Queries.GetNotifListByUserId;


public record GetNotifListByUserIdQuery() : IRequest<List<NotificationDto>>;

public class GetNotifListByUserIdQueryHandler : IRequestHandler<GetNotifListByUserIdQuery, List<NotificationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetNotifListByUserIdQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<NotificationDto>> Handle(GetNotifListByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications.Get<NotificationDto>(noty => noty.UserId == _currentUserService.Id);
    }
}
