using ECommerce.ProductManagement.Domain.ProductCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase.Configuration;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>, IDbModelConfiguration
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(q => q.Id);
        builder.HasMany(q=> q.Products)
            .WithOne()
            .HasForeignKey(q => q.CategoryId);
        builder.Property(q => q.Title).HasMaxLength(350);
    }
}
