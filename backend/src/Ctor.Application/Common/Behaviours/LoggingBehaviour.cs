using Ctor.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ctor.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime dateTime;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IDateTime dateTime)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        this.dateTime = dateTime;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Ctor Request: {@UserId} {@Request} at {@DateTime}",
            _currentUserService.Id, request, dateTime.Now);

        return await next();
    }
}
