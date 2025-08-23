using ECommerce.ProductManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase.Configuration;

public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>, IDbModelConfiguration
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(q => q.SpecificationTitle).HasMaxLength(500);
        builder.Property(q => q.SpecificationValue).HasMaxLength(1000);
        
        // builder.HasOne<Product>()
        //     .WithMany(product=> product.ProductSpecifications)
        //     .HasForeignKey(q => q.ProductId);
    }
}
