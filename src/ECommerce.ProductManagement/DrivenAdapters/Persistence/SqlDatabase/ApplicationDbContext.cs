using ECommerce.ProductManagement.Domain.ProductCategories;
using ECommerce.ProductManagement.Domain.Products;
using ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ProductSpecification> ProductSpecification { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IDbModelConfiguration).Assembly);
    }
}