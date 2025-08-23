using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Catalog.Models;

public class ProductCatalog
{
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid IntegrationProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<ProductSpecification> ProductSpecifications { get; set; } = new List<ProductSpecification>();
    public List<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
    public Guid IntegrationCategoryId { get; set; }
    public string CategoryTitle { get; set; }
}