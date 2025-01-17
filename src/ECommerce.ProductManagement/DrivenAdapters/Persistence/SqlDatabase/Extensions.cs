using ECommerce.ProductManagement.Domain.ProductCategories;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        var productCategoryRepository = services.GetRequiredService<IProductCategoryRepository>();
        try
        {
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context,productCategoryRepository);
        }
        catch
        {
            // ignored
        }
    }
}