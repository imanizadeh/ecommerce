using Grpc.Core;
using Grpc.Core.Interceptors;

namespace ECommerce.ProductManagement.DrivingAdapters.GrpcApi;

public class GrpcServerLoggerInterceptor(ILogger<GrpcServerLoggerInterceptor> logger) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error thrown by {Method}.", context.Method);
            throw;
        }
    }
}