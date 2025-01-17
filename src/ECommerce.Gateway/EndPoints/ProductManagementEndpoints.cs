using ECommerce.Gateway.Dtos.ProductsManagements;
using ECommerce.ProductManagement;
using ECommerce.ProductManagement.API.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Gateway.EndPoints;

public static class ProductManagementEndpoints
{
    public static void MapProductManagementEndpoints(this WebApplication app)
    {
        var productManagementGroup = app.MapGroup("/api/products-management")
            .WithTags("Products Management Api");

        productManagementGroup.MapPost("categories", async ([FromBody] AddCategoryDto addCategory,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            var product = await serviceClient.AddCategoryAsync(new AddCategoryRequest()
            {
                Title = addCategory.Title
            });
            return TypedResults.Ok(product);
        });

        productManagementGroup.MapGet("categories", async (
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            CategoryListRequest categoryListRequest = new()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var categories = await serviceClient.CategoryListAsync(categoryListRequest);
            return TypedResults.Ok(categories.CategoryItems);
        });

        productManagementGroup.MapGet("categories/{id}", async (
            [FromRoute] Guid id,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            GetCategoryRequest getCategoryRequest = new()
            {
                Id = id.ToString()
            };
            var category = await serviceClient.GetCategoryAsync(getCategoryRequest);
            return TypedResults.Ok(category);
        });

        productManagementGroup.MapPut("categories/{id}", async (
            [FromRoute] Guid id,
            [FromBody] EditCategoryDto editCategory,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            if (id != editCategory.Id)
            {
                TypedResults.BadRequest();
            }
            var category = await serviceClient.EditCategoryAsync(new EditCategoryRequest()
            {
                Id = editCategory.Id.ToString(),
                Title = editCategory.Title
            });
            return TypedResults.Ok(category);
        });

        productManagementGroup.MapDelete("categories/{id}", async (
            [FromRoute] Guid id,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            RemoveCategoryRequest removeCategoryRequest = new()
            {
                Id = id.ToString()
            };
            await serviceClient.RemoveCategoryAsync(removeCategoryRequest);
            return TypedResults.NoContent();
        });


        productManagementGroup.MapPost("products", async ([FromBody] AddProductDto addProduct,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            var product = await serviceClient.AddProductAsync(new AddProductRequest()
            {
                Description = addProduct.Description,
                Title = addProduct.Title,
                CategoryId = addProduct.CategoryId.ToString()
            });
            return TypedResults.Ok(product);
        });

        productManagementGroup.MapGet("products", async (
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            ProductListRequest productListRequest = new()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var products = await serviceClient.ProductListAsync(productListRequest);
            return TypedResults.Ok(products.ProductItems);
        });

        productManagementGroup.MapGet("products/{id}", async (
            [FromRoute] Guid id,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            GetProductRequest getProductRequest = new()
            {
                Id = id.ToString()
            };
            var product = await serviceClient.GetProductAsync(getProductRequest);
            return TypedResults.Ok(product);
        });

        productManagementGroup.MapPut("products/{id}", async (
            [FromRoute] Guid id,
            [FromBody] EditProductDto editProduct,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            if (id != editProduct.Id)
            {
                TypedResults.BadRequest();
            }
            var product = await serviceClient.EditProductAsync(new EditProductRequest()
            {
                Id = editProduct.Id.ToString(),
                Description = editProduct.Description,
                Title = editProduct.Title,
                CategoryId = editProduct.CategoryId.ToString()
            });
            return TypedResults.Ok(product);
        });

        productManagementGroup.MapDelete("products/{id}", async (
            [FromRoute] Guid id,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            RemoveProductRequest removeProductRequest = new()
            {
                Id = id.ToString()
            };
            await serviceClient.RemoveProductAsync(removeProductRequest);
            return TypedResults.NoContent();
        });

        productManagementGroup.MapPost("product-specifications", async ([FromBody] AddProductSpecificationDto addProductSpecification,
          [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            var productSpecification = await serviceClient.AddProductSpecificationAsync(new AddProductSpecificationRequest()
            {
                Priority = addProductSpecification.Priority,
                ProductId = addProductSpecification.ProductId.ToString(),
                SpecificationTitle = addProductSpecification.SpecificationTitle,
                SpecificationValue = addProductSpecification.SpecificationValue
            });
            return TypedResults.Ok(productSpecification);
        });

        productManagementGroup.MapGet("product-specifications/{id}", async (
            [FromRoute] Guid id,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            GetProductSpecificationRequest getProductSpecificationRequest = new()
            {
                Id = id.ToString()
            };
            var productSpecification = await serviceClient.GetProductSpecificationAsync(getProductSpecificationRequest);
            return TypedResults.Ok(productSpecification);
        });

        productManagementGroup.MapGet("product-specifications/{productId}/all", async (
            [FromRoute] Guid productId,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            ProductSpecificationListRequest id = new()
            {
                ProductId = productId.ToString()
            };
            var productSpecifications = await serviceClient.ProductSpecificationListAsync(id);
            return TypedResults.Ok(productSpecifications.ProductSpecificationItems);
        });

        productManagementGroup.MapPut("product-specifications/{id}", async (
            [FromRoute] Guid id,
            [FromBody] EditProductSpecificationDto editProductSpecificationDto,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            if (id != editProductSpecificationDto.Id)
            {
                TypedResults.BadRequest();
            }
            var productSpecification = await serviceClient.EditProductSpecificationAsync(new EditProductSpecificationRequest()
            {
                Id = editProductSpecificationDto.Id.ToString(),
                Priority = editProductSpecificationDto.Priority,
                ProductId = editProductSpecificationDto.ProductId.ToString(),
                SpecificationTitle = editProductSpecificationDto.SpecificationTitle,
                SpecificationValue = editProductSpecificationDto.SpecificationValue
            });
            return TypedResults.Ok(productSpecification);
        });

        productManagementGroup.MapDelete("product-specifications/{id}", async (
            [FromRoute] Guid id,
            [FromServices] ProductManagementService.ProductManagementServiceClient serviceClient) =>
        {
            RemoveProductSpecificationRequest removeProductSpecification = new()
            {
                Id = id.ToString()
            };
            await serviceClient.RemoveProductSpecificationAsync(removeProductSpecification);
            return TypedResults.NoContent();
        });
    }
}