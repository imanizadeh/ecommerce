using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Services;

namespace ECommerce.Catalog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<CatalogService>();
        builder.RegisterMongoDB();
        builder.RegisterMassTransit();
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapInventoryEndpoints();
        app.MapDefaultEndpoints();
        app.Run();
    }
}