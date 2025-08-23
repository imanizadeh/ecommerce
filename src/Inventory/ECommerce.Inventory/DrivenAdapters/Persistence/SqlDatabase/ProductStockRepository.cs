using ECommerce.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.DrivenAdapters.Persistence.SqlDatabase;

public class ProductStockRepository(ApplicationDbContext dbContext) : IProductStockRepository
{
    public ProductStock RegisterStock(ProductStock productStock)
    {
        dbContext.ProductsStock.Add(productStock);
        return productStock;
    }

    public ProductStock UpdateStock(ProductStock productStock)
    {
        dbContext.ProductsStock.Update(productStock);
        return productStock;
    }

    public async Task<ProductStock> GetStockByIdAsync(Guid id)
    {
        return await dbContext.ProductsStock.FirstOrDefaultAsync(stock => stock.Id == id);
    }
    
    public async Task<ProductStock> GetStockByProductIdAsync(Guid productId)
    {
        return await dbContext.ProductsStock.FirstOrDefaultAsync(stock => stock.ProductId == productId);
    }
}
