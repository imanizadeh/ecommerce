namespace ECommerce.Inventory.Domain;

public interface IProductStockRepository
{
    public ProductStock RegisterStock(ProductStock productStock);
    public ProductStock UpdateStock(ProductStock productStock);
    public Task<ProductStock> GetStockByIdAsync(Guid id);
    public Task<ProductStock> GetStockByProductIdAsync(Guid productId);
}