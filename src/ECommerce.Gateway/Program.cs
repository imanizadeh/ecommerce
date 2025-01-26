using ECommerce.Gateway.EndPoints;
using ECommerce.Gateway.Services;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Polly;
using Polly.Extensions.Http;

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

        builder.Services.AddScoped<InventoryService>();

        builder.Services.AddGrpcClient<ProductManagement.API.Grpc.ProductManagementService.ProductManagementServiceClient>(options =>
        {
            options.Address = new Uri(builder.Configuration.GetValue<string>("ProductManagement:ServerUrl"));
        });
        
        builder.Services.AddHttpClient("Inventory", httpClient =>
        {
            httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Inventory:ServerUrl"));
        })
        .AddPolicyHandler(GetCircuitBreakerPolicy());
        
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
        app.MapInventoryEndpoints();
        app.MapDefaultEndpoints();

        app.Run();
    }
    
    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}