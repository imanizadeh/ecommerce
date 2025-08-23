namespace ECommerce.ProductManagement.Domain.ProductCategories;

public interface IProductCategoryRepository
{
    public Task<ProductCategory> GetProductCategoryByIdAsync(Guid id);
    public Task<List<ProductCategory>> GetProductCategoriesAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    public ProductCategory AddProductCategory(ProductCategory productCategory);
    public ProductCategory EditProductCategory(ProductCategory productCategory);
    public void RemoveProductCategory(ProductCategory productCategory);
}
