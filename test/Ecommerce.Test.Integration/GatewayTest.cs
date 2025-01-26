using System.Net.Http.Json;
using System.Text.Json;
using Aspire.Hosting;
using ECommerce.Gateway.Dtos.Inventory;
using ECommerce.Gateway.Dtos.ProductsManagements;
using ECommerce.ProductManagement.API.Grpc;
using FluentAssertions;

namespace Ecommerce.Test.Integration;

public class GatewayTest
{
    [Fact]
    public async Task TestGetAllCategories()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        
        
        // Act
        var response = await httpClient.GetAsync("/api/products-management/categories?pageIndex=1&pageSize=10");
        var categories = await ToAModelAsync<List<SingleCategory>>(response);
    
        
        // Assert
        categories.Should().NotBeEmpty().And.HaveCountGreaterOrEqualTo(1);
    }
    
    [Fact]
    public async Task TestGetSingleCategory()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var categoriesResponse = await httpClient.GetAsync("/api/products-management/categories?pageIndex=1&pageSize=10");
        var categories = await ToAModelAsync<List<SingleCategory>>(categoriesResponse);
        var categoryId = categories.First().Id;
        
        // Act
        var response = await httpClient.GetAsync($"/api/products-management/categories/{categoryId}");
        var category = await ToAModelAsync<SingleCategory>(response);
        
        // Assert
        category.Id.Should().BeEquivalentTo(categoryId);
    }
    [Fact]
    public async Task TestCreateCategory()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var category = new AddCategoryDto()
        {
            Title = "Test Category"
        };
        
        // Act
        var response = await httpClient.PostAsJsonAsync("/api/products-management/categories", category);
        var returnedCategory = await ToAModelAsync<SingleCategory>(response);
        
        // Assert
        returnedCategory.Title.Should().BeEquivalentTo(category.Title);
    }
    
    [Fact]
    public async Task TestEditCategory()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var categoriesResponse = await httpClient.GetAsync("/api/products-management/categories?pageIndex=1&pageSize=10");
        var categories = await ToAModelAsync<List<SingleCategory>>(categoriesResponse);
        var categoryForEdit = new EditCategoryDto()
        {
            Title = "updated category",
            Id = Guid.Parse(categories.First().Id)
        };
    
        // Act
        var response = await httpClient.PutAsJsonAsync($"/api/products-management/categories/{categoryForEdit.Id}", categoryForEdit);
        var returnedCategory = await ToAModelAsync<SingleCategory>(response);
        
        // Assert
        categoryForEdit.Title.Should().BeEquivalentTo(returnedCategory.Title);
    }
    
    [Fact]
    public async Task TestRemoveCategory()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var categoriesResponse = await httpClient.GetAsync("/api/products-management/categories?pageIndex=1&pageSize=10");
        var categories = await ToAModelAsync<List<SingleCategory>>(categoriesResponse);
        
        // Act
        var response = await httpClient.DeleteAsync($"/api/products-management/categories/{categories.First().Id}");
       
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task TestGetAllProducts()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        
        
        // Act
        var response = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(response);
    
        
        // Assert
        products.Should().NotBeEmpty().And.HaveCountGreaterOrEqualTo(1);
    }
    
    [Fact]
    public async Task TestGetSingleProduct()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleCategory>>(productsResponse);
        var productId = products.First().Id;
        
        // Act
        var response = await httpClient.GetAsync($"/api/products-management/products/{productId}");
        var product = await ToAModelAsync<SingleProduct>(response);
        
        // Assert
        product.Id.Should().BeEquivalentTo(productId);
    }
    
    [Fact]
    public async Task TestCreateProduct()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var categoriesResponse = await httpClient.GetAsync("/api/products-management/categories?pageIndex=1&pageSize=10");
        var categories = await ToAModelAsync<List<SingleCategory>>(categoriesResponse);
        var categoryId = categories.First().Id;
        
        var product = new AddProductDto()
        {
            Title = "Test product title",
            Description = "Test product description",
            CategoryId = Guid.Parse(categoryId)
        };
        
        // Act
        var response = await httpClient.PostAsJsonAsync("/api/products-management/products", product);
        var returnedProduct = await ToAModelAsync<SingleProduct>(response);
        
        // Assert
        returnedProduct.Title.Should().BeEquivalentTo(product.Title);
    }
    
    [Fact]
    public async Task TestEditProduct()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productForEdit = new EditProductDto()
        {
            Title = "updated product title",
            Description = "updated product description",
            Id = Guid.Parse(products.First().Id),
            CategoryId = Guid.Parse(products.First().CategoryId)
        };
    
        // Act
        var response = await httpClient.PutAsJsonAsync($"/api/products-management/products/{productForEdit.Id}", productForEdit);
        var returnedProduct = await ToAModelAsync<SingleCategory>(response);
        
        // Assert
        productForEdit.Title.Should().BeEquivalentTo(returnedProduct.Title);
    }
    
    [Fact]
    public async Task TestRemoveProduct()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        
        // Act
        var response = await httpClient.DeleteAsync($"/api/products-management/products/{products.First().Id}");
       
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    
    [Fact]
    public async Task TestGetAllSpecification()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);

        var specification = new AddProductSpecificationDto()
        {
            ProductId = Guid.Parse(products[0].Id),
            SpecificationTitle = "spec title",
            SpecificationValue = "spec value",
            Priority = 10
        };
        await httpClient.PostAsJsonAsync("/api/products-management/product-specifications", specification);
        
        // Act
        var response = await httpClient.GetAsync($"/api/products-management/product-specifications/{products[0].Id}/all");
        var specifications = await ToAModelAsync<List<SingleProductSpecification>>(response);

        
        // Assert
        specifications.Should().NotBeEmpty().And.HaveCountGreaterOrEqualTo(1);
    }
    
    [Fact]
    public async Task TestGetSingleSpecification()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products.First().Id;
        var specification = new AddProductSpecificationDto()
        {
            ProductId = Guid.Parse(productId),
            SpecificationTitle = "spec title",
            SpecificationValue = "spec value",
            Priority = 10
        };
        var specResponse = await httpClient.PostAsJsonAsync("/api/products-management/product-specifications", specification);
        var addedSpec = await ToAModelAsync<SingleProductSpecification>(specResponse);
        
        // Act
        var response = await httpClient.GetAsync($"/api/products-management/product-specifications/{addedSpec.Id}");
        var spec = await ToAModelAsync<SingleProductSpecification>(response);
        
        // Assert
        addedSpec.Id.Should().BeEquivalentTo(spec.Id);
    }

    [Fact]
    public async Task TestCreateSpecification()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products[0].Id;
        var specification = new AddProductSpecificationDto()
        {
            ProductId = Guid.Parse(productId),
            SpecificationTitle = "spec title",
            SpecificationValue = "spec value",
            Priority = 10
        };
       
        
        // Act
        var specResponse = await httpClient.PostAsJsonAsync("/api/products-management/product-specifications", specification);
        var returnedSpec = await ToAModelAsync<SingleProductSpecification>(specResponse);
        
        // Assert
        returnedSpec.SpecificationTitle.Should().BeEquivalentTo(specification.SpecificationTitle);
    }
    
    [Fact]
    public async Task TestEditSpecification()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products[0].Id;
        var specification = new AddProductSpecificationDto()
        {
            ProductId = Guid.Parse(productId),
            SpecificationTitle = "spec title",
            SpecificationValue = "spec value",
            Priority = 10
        };
        var specResponse = await httpClient.PostAsJsonAsync("/api/products-management/product-specifications", specification);
        var returnedSpec = await ToAModelAsync<SingleProductSpecification>(specResponse);
        
        var specForEdit = new EditProductSpecificationDto()
        {
            Id = Guid.Parse(returnedSpec.Id),
            SpecificationTitle = "updated spec title",
            SpecificationValue = "updated spec value",
            ProductId = Guid.Parse(productId),
            Priority = 16
        };
        
        // Act
        var editedSpecResponse = await httpClient.PutAsJsonAsync($"/api/products-management/product-specifications/{specForEdit.Id}", specForEdit);
        var editedReturnedSpec = await ToAModelAsync<SingleProductSpecification>(editedSpecResponse);
        
        // Assert
        specForEdit.SpecificationValue.Should().BeEquivalentTo(editedReturnedSpec.SpecificationValue);
    }
    
    [Fact]
    public async Task TestRemoveSpecification()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products[0].Id;
        var specification = new AddProductSpecificationDto()
        {
            ProductId = Guid.Parse(productId),
            SpecificationTitle = "spec title",
            SpecificationValue = "spec value",
            Priority = 10
        };
        var specResponse = await httpClient.PostAsJsonAsync("/api/products-management/product-specifications", specification);
        var returnedSpec = await ToAModelAsync<SingleProductSpecification>(specResponse);
        
        
        // Act
        var response = await httpClient.DeleteAsync($"/api/products-management/product-specifications/{returnedSpec.Id}");
       
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task TestCreateProductStock()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products[0].Id;
        var stock = new AddStockDto()
        {
            ProductId = Guid.Parse(productId),
            ProductType = 1,
            SerialNumber = "test-serial-number",
            Color = 1,
            Discount = 12,
            Count = 10,
            Price = 1234000
        };
       
        
        // Act
        var stockResponse = await httpClient.PostAsJsonAsync("/api/inventory", stock);
        var returnedStock = await ToAModelAsync<Stock>(stockResponse);
        
        // Assert
        returnedStock.SerialNumber.Should().BeEquivalentTo(returnedStock.SerialNumber);
    }
    
    [Fact]
    public async Task TestEditProductStock()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products[0].Id;
        var stock = new AddStockDto()
        {
            ProductId = Guid.Parse(productId),
            ProductType = 1,
            SerialNumber = "test-serial-number",
            Color = 1,
            Discount = 12,
            Count = 10,
            Price = 1234000
        };
        var stockResponse = await httpClient.PostAsJsonAsync("/api/inventory", stock);
        var returnedStock = await ToAModelAsync<Stock>(stockResponse);
        
        
        var stockForEdit = new EditStockDto()
        {
            Id = returnedStock.Id,
            ProductId = returnedStock.ProductId,
            SerialNumber = "updated-serial-number",
            Color = 2,
            Discount = 9,
            Count = 20,
            Price = 800000,
            ProductType = 2
            
        };
        
        // Act
        var editedStockResponse = await httpClient.PutAsJsonAsync($"/api/inventory/{stockForEdit.Id}", stockForEdit);
        var editedReturnedStock = await ToAModelAsync<Stock>(editedStockResponse);
        
        // Assert
        stockForEdit.SerialNumber.Should().BeEquivalentTo(editedReturnedStock.SerialNumber);
    }
    
    [Fact]
    public async Task TestGetProductStockById()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products[0].Id;
        var stock = new AddStockDto()
        {
            ProductId = Guid.Parse(productId),
            ProductType = 1,
            SerialNumber = "test-serial-number",
            Color = 1,
            Discount = 12,
            Count = 10,
            Price = 1234000
        };
        var stockResponse = await httpClient.PostAsJsonAsync("/api/inventory", stock);
        var addedStock  = await ToAModelAsync<Stock>(stockResponse);
        
        
        // Act
        var response = await httpClient.GetAsync($"/api/inventory/{addedStock.Id}");
        var returnedStock = await ToAModelAsync<Stock>(response);
        
        // Assert
        addedStock.Id.Should().Be(returnedStock.Id);
    }
    
    [Fact]
    public async Task TestGetProductStockByProductId()
    {
        // Arrange
        await using var app = await GetApplication();
        var httpClient = app.CreateHttpClient("gateway");
        var productsResponse = await httpClient.GetAsync("/api/products-management/products?pageIndex=1&pageSize=10");
        var products = await ToAModelAsync<List<SingleProduct>>(productsResponse);
        var productId = products[0].Id;
        var stock = new AddStockDto()
        {
            ProductId = Guid.Parse(productId),
            ProductType = 1,
            SerialNumber = "test-serial-number",
            Color = 1,
            Discount = 12,
            Count = 10,
            Price = 1234000
        };
        var stockResponse = await httpClient.PostAsJsonAsync("/api/inventory", stock);
        var addedStock  = await ToAModelAsync<Stock>(stockResponse);
        
        
        // Act
        var response = await httpClient.GetAsync($"/api/inventory/{addedStock.ProductId}/stock");
        var returnedStock = await ToAModelAsync<Stock>(response);
        
        // Assert
        addedStock.ProductId.Should().Be(returnedStock.ProductId);
    }
    private static async Task<DistributedApplication> GetApplication()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.ECommerce_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();
        await resourceNotificationService.WaitForResourceAsync("gateway", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(60));
        return app;
    }
  
    private async Task<T?> ToAModelAsync<T>(HttpResponseMessage httpResponseMessage)
    {
        httpResponseMessage.EnsureSuccessStatusCode();
        var responseText = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseText, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}