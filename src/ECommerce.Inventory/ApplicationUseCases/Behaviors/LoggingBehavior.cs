using ECommerce.SharedFramework;
using MediatR;

namespace ECommerce.Inventory.ApplicationUseCases.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Start Handling Request {RequestName} ({@Request})", request.GetGenericTypeName(), request);
        var response = await next();
        logger.LogInformation("Request {RequestName} handled - response: {@Response}", request.GetGenericTypeName(), response);
        return response;
    }
}