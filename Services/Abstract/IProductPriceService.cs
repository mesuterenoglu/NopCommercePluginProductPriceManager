using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Mesut.ProductPriceManager.Services.Abstract;

public interface IProductPriceService
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    Task UpdateProductPricesByCategoriesAsync(int[] categories, bool increasePrices, decimal percentage);
    Task<IEnumerable<Product>> GetProductsByManufacturerAsync(int manufacturerId);
    Task UpdateProductPricesByManufacturersAsync(int[] manufacturers, bool increasePrices, decimal percentage);
}
