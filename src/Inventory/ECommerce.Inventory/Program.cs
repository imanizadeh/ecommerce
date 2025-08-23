using ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase;
using ECommerce.Inventory.DrivingAdapters.RestApi;

namespace ECommerce.Inventory;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();
        builder.RegisterDatabaseServices();
        builder.RegisterMediatR();
        builder.RegisterFluentValidator();
        builder.RegisterMassTransit();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.CreateDbIfNotExists();
        }

        app.UseHttpsRedirection();
        app.MapInventoryEndpoints();
        app.MapDefaultEndpoints();
        
        app.Run();
    }
}
