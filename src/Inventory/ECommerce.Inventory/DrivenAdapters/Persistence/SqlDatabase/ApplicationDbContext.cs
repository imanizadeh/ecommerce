using ECommerce.Inventory.Domain;
using ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ProductStock> ProductsStock { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IDbModelConfiguration).Assembly);
    }
}