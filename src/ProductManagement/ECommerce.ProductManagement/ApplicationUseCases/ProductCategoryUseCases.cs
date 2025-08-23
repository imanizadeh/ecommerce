using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using ECommerce.ProductManagement.ApplicationUseCases.Common;
using ECommerce.ProductManagement.Contracts;
using ECommerce.ProductManagement.Domain.ProductCategories;
using ECommerce.SharedFramework;
using MassTransit;
using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases;

public class ProductCategoryUseCases(
    IUnitOfWork unitOfWork,
    IProductCategoryRepository productCategoryRepository,
    ICacheService cacheService,
    IBus rabbitBus) :
    IRequestHandler<GetProductCategoryIdQuery, ProductCategoryQueryResult>,
    IRequestHandler<GetAllProductCategoriesQuery, List<ProductCategoryQueryResult>>,
    IRequestHandler<AddProductCategoryCommand, ProductCategoryQueryResult>,
    IRequestHandler<EditProductCategoryCommand, ProductCategoryQueryResult>,
    IRequestHandler<RemoveProductCategoryCommand>

{
    public async Task<ProductCategoryQueryResult> Handle(GetProductCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var existingProductCategory = await productCategoryRepository.GetProductCategoryByIdAsync(request.Id);
        if (existingProductCategory is null)
        {
            throw new Exception("Product category not found");
        }

        return new ProductCategoryQueryResult()
        {
            Id = existingProductCategory.Id,
            Title = existingProductCategory.Title
        };
    }

    public async Task<List<ProductCategoryQueryResult>> Handle(GetAllProductCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var productCategories =
            await productCategoryRepository.GetProductCategoriesAsync(request.PageIndex, request.PageSize,
                cancellationToken);
        return productCategories.Select(productCategory => new ProductCategoryQueryResult()
        {
            Id = productCategory.Id,
            Title = productCategory.Title
        }).ToList();
    }

    public async Task<ProductCategoryQueryResult> Handle(AddProductCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var productCategory = productCategoryRepository.AddProductCategory(new ProductCategory(command.Title));
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(new ProductCategoryCreatedEvent(productCategory.Id, productCategory.Title),
            cancellationToken);
        await cacheService.RemoveKeyAsync(CacheKeys.ProductCategories);
        return new ProductCategoryQueryResult()
        {
            Id = productCategory.Id,
            Title = productCategory.Title
        };
    }

    public async Task<ProductCategoryQueryResult> Handle(EditProductCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var existingProductCategory = await productCategoryRepository.GetProductCategoryByIdAsync(request.Id);
        if (existingProductCategory is null)
        {
            throw new Exception("Product category not found");
        }

        existingProductCategory.UpdateProductCategory(request.Title);
        productCategoryRepository.EditProductCategory(existingProductCategory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(
            new ProductCategoryEditedEvent(existingProductCategory.Id, existingProductCategory.Title),
            cancellationToken);
        await cacheService.RemoveKeyAsync(CacheKeys.ProductCategories);
        return new ProductCategoryQueryResult()
        {
            Id = existingProductCategory.Id,
            Title = existingProductCategory.Title
        };
    }

    public async Task Handle(RemoveProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingProductCategory = await productCategoryRepository.GetProductCategoryByIdAsync(request.Id);
        if (existingProductCategory is null)
        {
            throw new Exception("Product category not found");
        }

        productCategoryRepository.RemoveProductCategory(existingProductCategory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await rabbitBus.Publish(new ProductCategoryDeletedEvent(existingProductCategory.Id), cancellationToken);
        await cacheService.RemoveKeyAsync(CacheKeys.ProductCategories);
    }
}