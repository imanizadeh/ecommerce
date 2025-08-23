using System.Reflection.Metadata;
using ECommerce.ProductManagement.API.Grpc;
using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace ECommerce.ProductManagement.DrivingAdapters.GrpcApi;

public class ProductManagementService(
    ILogger<ProductManagementService> logger,
    IMediator mediator) : API.Grpc.ProductManagementService.ProductManagementServiceBase
{
    #region Category

    public override async Task<CategoryListResponse> CategoryList(CategoryListRequest request,
        ServerCallContext context)
    {
        var products = await mediator.Send(new GetAllProductCategoriesQuery()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        });
        var response = MapToCategoryListResponse(products);
        return response;
    }

    public override async Task<AddCategoryResponse> AddCategory(AddCategoryRequest request, ServerCallContext context)
    {
        var productCategory = await mediator.Send(new AddProductCategoryCommand()
        {
            Title = request.Title
        });
        var response = MapToAddCategoryResponse(productCategory);
        return response;
    }

    public override async Task<EditCategoryResponse> EditCategory(EditCategoryRequest request,
        ServerCallContext context)
    {
        var product = await mediator.Send(new EditProductCategoryCommand()
        {
            Id = Guid.Parse(request.Id),
            Title = request.Title
        });
        var response = MapToEditCategoryResponse(product);
        return response;
    }

    public override async Task<Empty> RemoveCategory(RemoveCategoryRequest request, ServerCallContext context)
    {
        await mediator.Send(new RemoveProductCategoryCommand()
        {
            Id = Guid.Parse(request.Id)
        });
        
        return await Task.FromResult(new Empty());
    }

    public override async Task<GetCategoryResponse> GetCategory(GetCategoryRequest request, ServerCallContext context)
    {
        var category = await mediator.Send(new GetProductCategoryIdQuery()
        {
            Id = Guid.Parse(request.Id)
        });
        
        if (category is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Category with id {request.Id} not found"));
        }

        var response = MapToGetCategoryResponse(category);
        return response;
    }

    private static CategoryListResponse MapToCategoryListResponse(List<ProductCategoryQueryResult> products)
    {
        var categoryDto = products.Select(product => new SingleCategory()
        {
            Id = product.Id.ToString(),
            Title = product.Title
        }).ToList();

        return new CategoryListResponse()
        {
            CategoryItems = { categoryDto }
        };
    }

    private static AddCategoryResponse MapToAddCategoryResponse(ProductCategoryQueryResult productCategory)
    {
        return new AddCategoryResponse
        {
            Id = productCategory.Id.ToString(),
            Title = productCategory.Title,
        };
    }

    private static EditCategoryResponse MapToEditCategoryResponse(ProductCategoryQueryResult product)
    {
        return new EditCategoryResponse
        {
            Id = product.Id.ToString(),
            Title = product.Title,
        };
    }

    private static GetCategoryResponse MapToGetCategoryResponse(ProductCategoryQueryResult product)
    {
        return new GetCategoryResponse
        {
            Id = product.Id.ToString(),
            Title = product.Title,
        };
    }

    #endregion

    #region Product

    public override async Task<ProductListResponse> ProductList(ProductListRequest request, ServerCallContext context)
    {
        var products = await mediator.Send(new ProductsQuery()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        });
        var response = MapToProductListResponse(products);
        return response;
    }

    public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var product = await mediator.Send(new GetProductByIdQuery()
        {
            Id =Guid.Parse(request.Id)
        });
        if (product is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with id {request.Id} not found"));
        }
        return MapToGetProductResponse(product);
    }

    public override async Task<AddProductResponse> AddProduct(AddProductRequest request, ServerCallContext context)
    {
        var command = MapToProductCommand(request);
        var addedProduct = await mediator.Send(new AddProductCommand()
        {
            Title = command.Title,
            Description = command.Description,
            CategoryId = command.CategoryId
        });
        var response = MapToAddProductResponse(addedProduct);
        return response;
    }

    public override async Task<EditProductResponse> EditProduct(EditProductRequest request, ServerCallContext context)
    {
        var editProductCommand = MapToEditProductCommand(request);
        var product = await mediator.Send(editProductCommand);

        var response = MapToEditProductResponse(product);
        return response;
    }

    public override async Task<Empty> RemoveProduct(RemoveProductRequest request, ServerCallContext context)
    {
        await mediator.Send(new RemoveProductCommand()
        {
            Id = Guid.Parse(request.Id)
        });
        return await Task.FromResult(new Empty());
    }


    private static ProductListResponse MapToProductListResponse(List<ProductQueryResult> products)
    {
        var productsDto = products.Select(product => new SingleProduct
        {
            CategoryId = product.CategoryId.ToString(),
            Description = product.Description,
            Id = product.Id.ToString(),
            Title = product.Title
        }).ToList();

        return new ProductListResponse()
        {
            ProductItems = { productsDto }
        };
    }

    private static AddProductResponse MapToAddProductResponse(ProductQueryResult addedProduct)
    {
        return new AddProductResponse
        {
            CategoryId = addedProduct.CategoryId.ToString(),
            Description = addedProduct.Description,
            Id = addedProduct.Id.ToString(),
            Title = addedProduct.Title
        };
    }

    private static AddProductCommand MapToProductCommand(AddProductRequest request)
    {
        var command = new AddProductCommand()
        {
            Title = request.Title,
            Description = request.Description,
            CategoryId = Guid.Parse(request.CategoryId)
        };
        return command;
    }

    private static EditProductResponse MapToEditProductResponse(ProductQueryResult product)
    {
        return new EditProductResponse
        {
            CategoryId = product.CategoryId.ToString(),
            Description = product.Description,
            Id = product.Id.ToString(),
            Title = product.Title,
        };
    }

    private static EditProductCommand MapToEditProductCommand(EditProductRequest request)
    {
        EditProductCommand editProductCommand = new()
        {
            Title = request.Title,
            Description = request.Description,
            Id =Guid.Parse(request.Id),
            CategoryId = Guid.Parse(request.CategoryId)
        };
        return editProductCommand;
    }

    private static GetProductResponse MapToGetProductResponse(ProductQueryResult product)
    {
        return new GetProductResponse
        {
            CategoryId = product.CategoryId.ToString(),
            Description = product.Description,
            Id = product.Id.ToString(),
            Title = product.Title,
        };
    }

    #endregion

    #region Specification

    public override async Task<AddProductSpecificationResponse> AddProductSpecification(
        AddProductSpecificationRequest request, ServerCallContext context)
    {
        var addCommand = MapToAddProductSpecificationCommand(request);
        var addedProductSpecification = await mediator.Send(addCommand);
        var response = MapToAddProductSpecificationResponse(addedProductSpecification);
        return response;
    }

    public override async Task<EditProductSpecificationResponse> EditProductSpecification(
        EditProductSpecificationRequest request, ServerCallContext context)
    {
        var editCommand = MapToEditProductSpecificationCommand(request);
        var updatedSpecification = await mediator.Send(editCommand);
        var response = MapEditProductSpecificationResponse(updatedSpecification);
        return response;
    }

    public override async Task<GetProductSpecificationResponse> GetProductSpecification(
        GetProductSpecificationRequest request, ServerCallContext context)
    {
        var productsSpecification = await mediator.Send(new GetProductSpecificationByIdQuery()
        {
            Id =Guid.Parse(request.Id)
        });
        if (productsSpecification is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $" Specification with id {request.Id} not found"));
        }
        var response = MapGetProductSpecificationResponse(productsSpecification);
        return response;
    }

    public override async Task<ProductSpecificationListResponse> ProductSpecificationList(
        ProductSpecificationListRequest request, ServerCallContext context)
    {
        var productsSpecifications = await mediator.Send(new GetProductSpecificationByProductIdQuery()
        {
            ProductId =Guid.Parse(request.ProductId)
        });

        var response = MapToProductSpecificationListResponse(productsSpecifications);
        return response;
    }

    public override async Task<Empty> RemoveProductSpecification(RemoveProductSpecificationRequest request,
        ServerCallContext context)
    {
        await mediator.Send(new RemoveProductSpecificationCommand()
        {
            Id =Guid.Parse(request.Id)
        });
        return await Task.FromResult(new Empty());
    }

    private static AddProductSpecificationCommand MapToAddProductSpecificationCommand(
        AddProductSpecificationRequest request)
    {
        var addProductCommand = new AddProductSpecificationCommand()
        {
            ProductId =Guid.Parse(request.ProductId),
            SpecificationTitle = request.SpecificationTitle,
            SpecificationValue = request.SpecificationValue,
            Priority = request.Priority
        };
        return addProductCommand;
    }

    private static AddProductSpecificationResponse MapToAddProductSpecificationResponse(
        ProductSpecificationQueryResult addedProductSpecification)
    {
        return new AddProductSpecificationResponse
        {
            SpecificationTitle = addedProductSpecification.SpecificationTitle,
            SpecificationValue = addedProductSpecification.SpecificationValue,
            Priority = addedProductSpecification.Priority,
            Id = addedProductSpecification.Id.ToString(),
            ProductId = addedProductSpecification.ProductId.ToString()
        };
    }

    private static EditProductSpecificationResponse MapEditProductSpecificationResponse(
        ProductSpecificationQueryResult updatedSpecification)
    {
        return new EditProductSpecificationResponse
        {
            SpecificationTitle = updatedSpecification.SpecificationTitle,
            SpecificationValue = updatedSpecification.SpecificationValue,
            Id = updatedSpecification.Id.ToString(),
            Priority = updatedSpecification.Priority,
            ProductId = updatedSpecification.ProductId.ToString()
        };
    }

    private static EditProductSpecificationCommand MapToEditProductSpecificationCommand(
        EditProductSpecificationRequest request)
    {
        EditProductSpecificationCommand editCommand = new()
        {
            SpecificationTitle = request.SpecificationTitle,
            SpecificationValue = request.SpecificationValue,
            Id = Guid.Parse(request.Id),
            Priority = request.Priority,
            ProductId =Guid.Parse(request.ProductId)
        };
        return editCommand;
    }

    private static GetProductSpecificationResponse MapGetProductSpecificationResponse(
        ProductSpecificationQueryResult productsSpecification)
    {
        return new GetProductSpecificationResponse
        {
            Id = productsSpecification.Id.ToString(),
            ProductId = productsSpecification.ProductId.ToString(),
            Priority = productsSpecification.Priority,
            SpecificationTitle = productsSpecification.SpecificationTitle,
            SpecificationValue = productsSpecification.SpecificationValue
        };
    }

    private static ProductSpecificationListResponse MapToProductSpecificationListResponse(
        List<ProductSpecificationQueryResult> productsSpecifications)
    {
        var productSpecificationDtos = productsSpecifications.Select(productsSpecification =>
            new SingleProductSpecification()
            {
                Id = productsSpecification.Id.ToString(),
                ProductId = productsSpecification.ProductId.ToString(),
                Priority = productsSpecification.Priority,
                SpecificationTitle = productsSpecification.SpecificationTitle,
                SpecificationValue = productsSpecification.SpecificationValue
            }).ToList();

        return new ProductSpecificationListResponse()
        {
            ProductSpecificationItems = { productSpecificationDtos }
        };
    }

    #endregion
}