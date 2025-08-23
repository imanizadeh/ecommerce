using ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase;
using ECommerce.ProductManagement.DrivingAdapters.GrpcApi;


namespace ECommerce.ProductManagement;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();
        builder.RegisterDatabaseServices();
        builder.RegisterCachingService();
        builder.RegisterMediatR();
        builder.RegisterFluentValidator();
        builder.RegisterMassTransit();

        builder.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
            options.Interceptors.Add<GrpcServerLoggerInterceptor>();
        });

        var app = builder.Build();
        if (builder.Environment.IsDevelopment())
        {
            app.CreateDbIfNotExists();
        }

        app.UseHttpsRedirection();
        app.MapGrpcService<ProductManagementService>();
        app.MapDefaultEndpoints();

        app.Run();
    }
}