using ECommerce.ProductManagement.Domain.ProductCategories;
using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.Domain.Products;

public class Product : BaseEntity<Guid>, IAggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }

    private List<ProductSpecification> _productSpecifications;
    public IReadOnlyCollection<ProductSpecification> ProductSpecifications => _productSpecifications?.AsReadOnly();

    private Product(string title, string description, Guid categoryId)
    {
        Id = Guid.NewGuid();
        _productSpecifications = new List<ProductSpecification>();
        Title = title;
        Description = description;
        CategoryId = categoryId;
    }

    public static async Task<Product> CreateAsync(string title,
        string description,
        Guid categoryId,
        IProductCategoryRepository categoryRepository)
    {
        await ValidateProductAsync(title, description, categoryId, categoryRepository);
        return new Product(title, description, categoryId);
    }

    public async Task UpdateProductDataAsync(string title,
        string description,
        Guid categoryId,
        IProductCategoryRepository categoryRepository)
    {
        await ValidateProductAsync(title, description, categoryId, categoryRepository);

        Title = title;
        Description = description;
        CategoryId = categoryId;
    }

    private static async Task ValidateProductAsync(string title,
        string description,
        Guid categoryId,
        IProductCategoryRepository categoryRepository)
    {
        var productCategory = await categoryRepository.GetProductCategoryByIdAsync(categoryId);
        if (productCategory is null)
        {
            throw new DomainException($"Product category with id {categoryId} not found");
        }

        if (title is null)
        {
            throw new DomainException($"Product title is required.");
        }

        if (description is null)
        {
            throw new DomainException($"Product description is required.");
        }
    }
}