using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Catalog.Models;

public class ProductCategory
{
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid IntegrationCategoryId { get; set; }
    public string Title { get; set; }
}