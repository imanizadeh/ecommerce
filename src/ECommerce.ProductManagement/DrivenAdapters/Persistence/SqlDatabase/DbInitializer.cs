using ECommerce.ProductManagement.Domain.ProductCategories;
using ECommerce.ProductManagement.Domain.Products;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context,
        IProductCategoryRepository productCategoryRepository)
    {
        if (context.Products.Any())
            return;

        var productsCategories = new List<ProductCategory>
        {
            new("outdoor"),
            new("usb-hub")
        };
        context.AddRange(productsCategories);
        context.SaveChanges();

        var solarPoweredFlashlight = Product.CreateAsync("Solar Powered Flashlight",
            "A fantastic product for outdoor enthusiasts",
            productsCategories.First().Id,
            productCategoryRepository).Result;

        var hikingPoles = Product.CreateAsync("Hiking Poles",
            "Ideal for camping and hiking trips",
            productsCategories.First().Id,
            productCategoryRepository).Result;

        context.AddRange(solarPoweredFlashlight, hikingPoles);
        context.SaveChanges();
    }
}