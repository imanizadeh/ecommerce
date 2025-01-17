using ECommerce.ProductManagement.ApplicationUseCases.Common;
using ECommerce.ProductManagement.Domain.ProductCategories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.SqlDatabase;

public class ProductCategoryRepository(ApplicationDbContext dbContext,ICacheService cacheService) : IProductCategoryRepository
{
    public async Task<ProductCategory> GetProductCategoryByIdAsync(Guid id)
    {
        var categories = await GetCategoriesFromCacheAsync();
        return categories.FirstOrDefault(product => product.Id == id);
    }
    public async Task<List<ProductCategory>> GetProductCategoriesAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var categories = await GetCategoriesFromCacheAsync();
        return categories
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
    public ProductCategory AddProductCategory(ProductCategory productCategory)
    {
        dbContext.ProductCategories.Add(productCategory);
        return productCategory;
    }
    public ProductCategory EditProductCategory(ProductCategory productCategory)
    {
        dbContext.ProductCategories.Update(productCategory);
        return productCategory;
    }
    public void RemoveProductCategory(ProductCategory productCategory)
    {
        dbContext.ProductCategories.Remove(productCategory);
    }
    private async Task<List<ProductCategory>?> GetCategoriesFromCacheAsync()
    {
       return await cacheService.GetOrSetCollectionsAsync(
           CacheKeys.ProductCategories,
           TimeSpan.FromDays(30), 
           async () =>
                    await dbContext.ProductCategories.ToListAsync()
           );
    }
}
