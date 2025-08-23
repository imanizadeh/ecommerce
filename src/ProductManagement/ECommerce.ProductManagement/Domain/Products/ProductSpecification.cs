using System.Diagnostics.Contracts;
using ECommerce.ProductManagement.Domain.ProductCategories;
using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.Domain.Products;

public class ProductSpecification : BaseEntity<Guid>
{
    public string SpecificationTitle { get; private set; }
    public string SpecificationValue { get; private set; }
    public int Priority { get; private set; }
    public Guid ProductId { get; private set; }

    public ProductSpecification()
    {
        //just to messagepack serialization
    }

    private ProductSpecification(Guid productId, string specificationTitle, string specificationValue, int priority)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        SpecificationTitle = specificationTitle;
        SpecificationValue = specificationValue;
        Priority = priority;
    }

    public static async Task<ProductSpecification> CreateAsync(
        Guid productId,
        string specificationTitle,
        string specificationValue,
        int priority,
        IProductRepository productRepository)
    {
        await ValidateProductSpecificationAsync(
            productId,
            specificationTitle,
            specificationValue,
            productRepository);

        return new ProductSpecification(productId, specificationTitle, specificationValue, priority);
    }

    public async Task UpdateDataAsync(
        Guid productId,
        string specificationTitle,
        string specificationValue,
        int priority,
        IProductRepository productRepository)
    {
        await ValidateProductSpecificationAsync(
            productId,
            specificationTitle,
            specificationValue,
            productRepository);

        ProductId = productId;
        SpecificationTitle = specificationTitle;
        SpecificationValue = specificationValue;
        Priority = priority;
    }

    private static async Task ValidateProductSpecificationAsync(
        Guid productId,
        string specificationTitle,
        string specificationValue,
        IProductRepository productRepository)
    {
        var product = await productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            throw new DomainException($"Product with id {productId} not found");
        }

        if (specificationTitle is null)
        {
            throw new DomainException($"Specification title is required.");
        }

        if (specificationValue is null)
        {
            throw new DomainException($"Specification value is required.");
        }
    }
}