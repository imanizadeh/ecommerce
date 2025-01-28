using ECommerce.Catalog.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace ECommerce.Catalog.Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProductCategory> ProductCategories { get; init; }
    public DbSet<ProductCatalog> ProductsCatalog { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductCategory>().ToCollection("product-categories");
        modelBuilder.Entity<ProductCatalog>().ToCollection("products-catalog");
    }
}