using Nop.Core.Caching;

namespace Nop.Plugin.Mesut.ProductPriceManager;

public class ProductPriceManagerConstants
{
    public static CacheKey CategoryProductsCacheKey => new("ProductPriceManager.Category-{0}", PrefixCacheKey);
    public static string PrefixCacheKey => "Nop.Plugin.Mesut.ProductPriceManager";
    public static CacheKey ManufacturerProductsCacheKey => new("ProductPriceManager.Manufacturer-{0}", PrefixCacheKey);
}
