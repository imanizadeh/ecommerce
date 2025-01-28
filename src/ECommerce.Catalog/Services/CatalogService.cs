using ECommerce.Catalog.Dtos;
using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Models;

namespace ECommerce.Catalog.Services;

public class CatalogService(ApplicationDbContext dbContext)
{
    public async Task<List<ProductItem>> GetProductsAsync(GetProductsQuery query)
    {
        var productsQuery = dbContext.ProductsCatalog
            .Where(x => x.ProductStocks.Count > 0).ToList()
            .Select(x => SelectProductItem(x, query)).AsQueryable();


        if (query.SortType == SortType.PriceAscending)
        {
            productsQuery = productsQuery.OrderBy(x => x.Price);
        }

        productsQuery = query.SortType switch
        {
            SortType.PriceAscending => productsQuery.OrderBy(x => x.Price),
            SortType.PriceDescending => productsQuery.OrderByDescending(x => x.Price),
            SortType.CountAscending => productsQuery.OrderBy(x => x.Count),
            SortType.CountDescending => productsQuery.OrderByDescending(x => x.Count),
            _ => productsQuery.OrderBy(x => x.Price)
        };

        return productsQuery.ToList();
    }


    private static ProductItem SelectProductItem(ProductCatalog x, GetProductsQuery query)
    {
        var stocks = x.ProductStocks.ToList();
        
        if (!query.Location.Equals("Iran", StringComparison.OrdinalIgnoreCase))
        {
            stocks = stocks.Where(s => s.ProductType.Equals("Original", StringComparison.OrdinalIgnoreCase)).ToList();
        }
        
        if (!query.IsAuthenticated)
        {
            stocks = stocks.Where(s => s.Discount <= 0).ToList();
        }

        var productStock = stocks.OrderBy(p => p.Price).FirstOrDefault();

        return new ProductItem()
        {
            Id = x.Id.ToString(),
            Discount = productStock?.Discount ?? 0,
            Price = productStock?.Price ?? 0,
            Title = x.Title,
            Count = productStock?.Count ?? 0,
            SerialNumber = productStock?.SerialNumber ?? string.Empty
        };
    }
}