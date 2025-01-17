namespace ECommerce.ProductManagement.ApplicationUseCases.Common;

public static class CacheKeys
{
    private const string CacheKeyPrefix = "ecommerce:product_management:";
    public const string ProductCategories = $"{CacheKeyPrefix}product_categories";
    public const string ProductSpecifications = $"{CacheKeyPrefix}product_specifications";
}