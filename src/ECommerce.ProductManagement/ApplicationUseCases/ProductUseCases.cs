using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using ECommerce.ProductManagement.ApplicationUseCases.Common;
using ECommerce.ProductManagement.ApplicationUseCases.IntegrationEvents;
using ECommerce.ProductManagement.Domain.ProductCategories;
using ECommerce.ProductManagement.Domain.Products;
using ECommerce.SharedFramework;
using MassTransit;
using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases;

public class ProductUseCases (
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    IProductCategoryRepository productCategoryRepository,
    ICacheService cacheService,
    IBus rabbitBus) :
    IRequestHandler<ProductsQuery, List<ProductQueryResult>>,
    IRequestHandler<GetProductByIdQuery, ProductQueryResult>,
    IRequestHandler<AddProductCommand, ProductQueryResult>,
    IRequestHandler<EditProductCommand, ProductQueryResult>,
    IRequestHandler<RemoveProductCommand>,
    IRequestHandler<GetProductSpecificationByIdQuery,ProductSpecificationQueryResult>,
    IRequestHandler<GetProductSpecificationByProductIdQuery,List<ProductSpecificationQueryResult>>,
    IRequestHandler<AddProductSpecificationCommand, ProductSpecificationQueryResult>,
    IRequestHandler<EditProductSpecificationCommand, ProductSpecificationQueryResult>,
    IRequestHandler<RemoveProductSpecificationCommand>
{
    public async Task<List<ProductQueryResult>> Handle(ProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsAsync(query.PageIndex, query.PageSize, cancellationToken);
        return products.Select(product => new ProductQueryResult()
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Title = product.Title
        }).ToList();
    }
    
    public async Task<ProductQueryResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.Id);
        if (product is null)
        {
            throw new Exception("Product not found");
        }
        return new ProductQueryResult()
        {
            Id = request.Id,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Title = product.Title
        };
    }

    public async Task<ProductQueryResult> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var product = await Product.CreateAsync(command.Title,
            command.Description,
            command.CategoryId,
            productCategoryRepository);
        
        productRepository.AddProduct(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(new ProductCreatedEvent(product.Id, product.Title, product.Description, product.CategoryId),
            cancellationToken);
        return new ProductQueryResult()
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Title = product.Title
        };
    }

    public async Task<ProductQueryResult> Handle(EditProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetProductByIdAsync(command.Id);
        if (existingProduct is null)
        {
            throw new Exception("Product not found");
        }

        await existingProduct.UpdateProductDataAsync(command.Title,
            command.Description,
            command.CategoryId,
            productCategoryRepository);

        productRepository.EditProduct(existingProduct);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(new ProductEditedEvent(existingProduct.Id, existingProduct.Title, existingProduct.Description, existingProduct.CategoryId),
            cancellationToken);
        return new ProductQueryResult()
        {
            Id = existingProduct.Id,
            CategoryId = existingProduct.CategoryId,
            Description = existingProduct.Description,
            Title = existingProduct.Title
        };
    }

    public async Task Handle(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetProductByIdAsync(command.Id);
        if (existingProduct is null)
        {
            throw new Exception("Product not found");
        }
        productRepository.RemoveProduct(existingProduct);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(new ProductDeletedEvent(existingProduct.Id), cancellationToken);
    }

    public async Task<ProductSpecificationQueryResult> Handle(GetProductSpecificationByIdQuery request, CancellationToken cancellationToken)
    {
        var productSpecification = await productRepository.GetProductSpecificationByIdAsync(request.Id);
        if (productSpecification is null)
        {
            throw new Exception("Specification not found");
        }
        return new ProductSpecificationQueryResult()
        {
            Id = productSpecification.Id,
            ProductId = productSpecification.ProductId,
            Priority = productSpecification.Priority,
            SpecificationTitle = productSpecification.SpecificationTitle,
            SpecificationValue = productSpecification.SpecificationValue
        };
    }

    public async Task<List<ProductSpecificationQueryResult>> Handle(GetProductSpecificationByProductIdQuery request, CancellationToken cancellationToken)
    {
        var productSpecifications = await productRepository.GetProductSpecificationByProductIdAsync(request.ProductId);
        return productSpecifications.Select(q => new ProductSpecificationQueryResult()
        {
            Id = q.Id,
            ProductId = q.ProductId,
            Priority = q.Priority,
            SpecificationTitle = q.SpecificationTitle,
            SpecificationValue = q.SpecificationValue
        }).ToList();
    }

    public async Task<ProductSpecificationQueryResult> Handle(AddProductSpecificationCommand command, CancellationToken cancellationToken)
    {
        var productSpecification = await ProductSpecification.CreateAsync(command.ProductId, command.SpecificationTitle,
            command.SpecificationValue, command.Priority, productRepository);
        
        productRepository.AddProductSpecification(productSpecification);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(new ProductSpecCreatedEvent(productSpecification.Id,
            productSpecification.SpecificationTitle,
            productSpecification.SpecificationValue,
            productSpecification.Priority,
            productSpecification.ProductId), cancellationToken);
        await cacheService.RemoveKeyAsync(CacheKeys.ProductSpecifications);
        return new ProductSpecificationQueryResult()
        {
            Id = productSpecification.Id,
            ProductId = productSpecification.ProductId,
            Priority = productSpecification.Priority,
            SpecificationTitle = productSpecification.SpecificationTitle,
            SpecificationValue = productSpecification.SpecificationValue
        };
    }

    public async Task<ProductSpecificationQueryResult> Handle(EditProductSpecificationCommand command, CancellationToken cancellationToken)
    {
        var existingProductSpecification = await productRepository.GetProductSpecificationByIdAsync(command.Id);
        if (existingProductSpecification is null)
        {
            throw new Exception("Product specification not found");
        }

        await existingProductSpecification.UpdateDataAsync(command.ProductId,
            command.SpecificationTitle,
            command.SpecificationValue,
            command.Priority,
            productRepository);

        productRepository.UpdateProductSpecification(existingProductSpecification);
        await unitOfWork.SaveChangesAsync();
        await rabbitBus.Publish(new ProductSpecEditedEvent(existingProductSpecification.Id,
            existingProductSpecification.SpecificationTitle,
            existingProductSpecification.SpecificationValue,
            existingProductSpecification.Priority,
            existingProductSpecification.ProductId), cancellationToken);
        await cacheService.RemoveKeyAsync(CacheKeys.ProductSpecifications);
        return  new ProductSpecificationQueryResult()
        {
            Id = existingProductSpecification.Id,
            ProductId = existingProductSpecification.ProductId,
            Priority = existingProductSpecification.Priority,
            SpecificationTitle = existingProductSpecification.SpecificationTitle,
            SpecificationValue = existingProductSpecification.SpecificationValue
        };
    }

    public async Task Handle(RemoveProductSpecificationCommand command, CancellationToken cancellationToken)
    {
        var existingProductSpecification = await productRepository.GetProductSpecificationByIdAsync(command.Id);
        if (existingProductSpecification is null)
        {
            throw new Exception("Product specification not found");
        }
        productRepository.RemoveProductSpecification(existingProductSpecification);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(new ProductSpecDeletedEvent(existingProductSpecification.Id), cancellationToken);
        await cacheService.RemoveKeyAsync(CacheKeys.ProductSpecifications);
    }
}