using ECommerce.ProductManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>, IDbModelConfiguration
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Title).HasMaxLength(350);
        builder.Property(q => q.Description).HasMaxLength(1500);
        builder.HasMany(q=> q.ProductSpecifications)
            .WithOne()
            .HasForeignKey(q => q.ProductId);

    }
}
