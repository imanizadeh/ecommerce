using ECommerce.Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase.Configuration;

public class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>, IDbModelConfiguration
{
    public void Configure(EntityTypeBuilder<ProductStock> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.SerialNumber).HasMaxLength(350);
        builder.Property(q => q.Color).IsRequired();
        builder.Property(q => q.ProductType).IsRequired();
        builder.Property(q => q.Price).IsRequired();
    }
}
