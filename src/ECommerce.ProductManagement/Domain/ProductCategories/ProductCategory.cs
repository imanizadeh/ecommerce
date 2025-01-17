using ECommerce.ProductManagement.Domain.Products;
using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.Domain.ProductCategories;

public class ProductCategory : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; private set; }
    
    private List<Product> _products;
    public IReadOnlyCollection<Product> Products => _products?.AsReadOnly();
    public ProductCategory(string title)
    {
        _products = new List<Product>();
        Id = Guid.NewGuid();
        if (title is null)
        {
            throw new DomainException("Category title is required.");
        }
        
        Title = title;
    }
    public void UpdateProductCategory(string title)
    {
        Title = title;
    }
}
