using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ProductManagement.Domain.Products;

public interface IProductRepository
{
    public Task<Product> GetProductByIdAsync(Guid id);
    public Task<List<Product>> GetProductsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    public Product AddProduct(Product product);
    public Product EditProduct(Product product);
    public void RemoveProduct(Product product);
    public ProductSpecification AddProductSpecification(ProductSpecification productSpecification);
    public ProductSpecification UpdateProductSpecification(ProductSpecification productSpecification);
    public void RemoveProductSpecification(ProductSpecification productSpecification);
    public Task<ProductSpecification> GetProductSpecificationByIdAsync(Guid id);
    public Task<List<ProductSpecification>> GetProductSpecificationByProductIdAsync(Guid productId);
}
