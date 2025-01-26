using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        try
        {
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw(context.Database.GenerateCreateScript().Replace("GO",string.Empty));
        }
        catch
        {
            //ignore
        }
    }
}