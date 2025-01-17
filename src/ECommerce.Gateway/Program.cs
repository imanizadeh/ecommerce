using ECommerce.Gateway.EndPoints;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace ECommerce.Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddGrpcClient<ProductManagement.API.Grpc.ProductManagementService.ProductManagementServiceClient>(options =>
        {
            options.Address = new Uri(builder.Configuration.GetValue<string>("ProductManagement:ServerUrl"));
        });
        
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapProductManagementEndpoints();
        app.MapDefaultEndpoints();

        app.Run();
    }
}