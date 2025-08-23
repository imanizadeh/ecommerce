using ECommerce.ProductManagement.ApplicationUseCases.Common;
using ECommerce.ProductManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase;

public class ProductRepository(ApplicationDbContext dbContext,
    ICacheService cacheService) : IProductRepository
{
    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        return await dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task<List<Product>> GetProductsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Skip((pageIndex -1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    public Product AddProduct(Product product)
    {
        dbContext.Products.Add(product);
        return product;
    }
    public Product EditProduct(Product product)
    {
        dbContext.Products.Update(product);
        return product;
    }
    public void RemoveProduct(Product product)
    {
        dbContext.Products.Remove(product);
    }

    public ProductSpecification AddProductSpecification(ProductSpecification productSpecification)
    {
        dbContext.ProductSpecification.Add(productSpecification);
        return productSpecification;
    }

    public ProductSpecification UpdateProductSpecification(ProductSpecification productSpecification)
    {
        dbContext.ProductSpecification.Update(productSpecification);
        return productSpecification;
    }

    public void RemoveProductSpecification(ProductSpecification productSpecification)
    {
        dbContext.ProductSpecification.Remove(productSpecification);
    }

    public async Task<ProductSpecification> GetProductSpecificationByIdAsync(Guid id)
    {
        try
        {
            var specifications = await GetSpecificationsFromCacheAsync();
            return specifications.FirstOrDefault(spec => spec.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<List<ProductSpecification>> GetProductSpecificationByProductIdAsync(Guid productId)
    {
        var specifications = await GetSpecificationsFromCacheAsync();
        return specifications.Where(spec => spec.ProductId == productId).ToList();
    }

    private async Task<List<ProductSpecification>?> GetSpecificationsFromCacheAsync()
    {
        return await cacheService.GetOrSetCollectionsAsync(
            CacheKeys.ProductSpecifications,
            TimeSpan.FromDays(30), 
            async () => await dbContext.ProductSpecification.ToListAsync()
        );
    }
}
