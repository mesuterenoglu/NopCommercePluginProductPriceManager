using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Plugin.Mesut.ProductPriceManager.Services.Abstract;

namespace Nop.Plugin.Mesut.ProductPriceManager.Services;

public class ProductPriceService : IProductPriceService
{
    #region Fields
    private readonly IRepository<Product> _repository;
    private readonly IRepository<ProductCategory> _productCategoryRepository;
    private readonly IRepository<ProductManufacturer> _productManufacturerRepository;
    private readonly IStaticCacheManager _staticCacheManager;

    #endregion

    #region Ctor
    public ProductPriceService(IRepository<Product> repository,
        IRepository<ProductCategory> productCategoryRepository,
        IRepository<ProductManufacturer> productManufacturerRepository,
        IStaticCacheManager staticCacheManager)
    {
        _repository = repository;
        _productCategoryRepository = productCategoryRepository;
        _productManufacturerRepository = productManufacturerRepository;
        _staticCacheManager = staticCacheManager;
    }
    #endregion

    #region Methods
    public async Task UpdateProductPricesByCategoriesAsync(int[] categories, bool increasePrices, decimal percentage)
    {
        var products = new List<Product>();
        foreach (var category in categories)
        {
            var categoryProducts = await GetProductsByCategoryAsync(category);
            products.AddRange(categoryProducts);
        }
        UpdateProductPrices(products, increasePrices, percentage);
    }

    public async Task UpdateProductPricesByManufacturersAsync(int[] manufacturers, bool increasePrices, decimal percentage)
    {
        var products = new List<Product>();
        foreach (var manufacturer in manufacturers)
        {
            var manufacturerProducts = await GetProductsByManufacturerAsync(manufacturer);
            products.AddRange(manufacturerProducts);
        }
        UpdateProductPrices(products, increasePrices, percentage);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        IList<Product> categoryProducts = new List<Product>();

        if (categoryId == 0)
            return categoryProducts;

        var cacheKey = _staticCacheManager.PrepareKey(ProductPriceManagerConstants.CategoryProductsCacheKey, categoryId);

        var categoryProductIds = await _staticCacheManager.GetAsync(cacheKey, () =>
        {
            var query = from p in _repository.Table
                        join pc in _productCategoryRepository.Table on p.Id equals pc.ProductId
                        where p.Published &&
                              !p.Deleted &&
                              p.VisibleIndividually &&
                              categoryId == pc.CategoryId
                        select p;

            categoryProducts = query.ToList();

            return categoryProducts.Select(p => p.Id).ToList();
        });

        if (categoryProducts.Count == 0 && categoryProductIds.Count > 0)
            categoryProducts = await _repository.GetByIdsAsync(categoryProductIds, cache => default, false);

        return categoryProducts;
    }

    public async Task<IEnumerable<Product>> GetProductsByManufacturerAsync(int manufacturerId)
    {
        IList<Product> manufacturerProducts = new List<Product>();

        if (manufacturerId == 0)
            return manufacturerProducts;

        var cacheKey = _staticCacheManager.PrepareKey(ProductPriceManagerConstants.ManufacturerProductsCacheKey, manufacturerId);

        var manufacturerProductIds = await _staticCacheManager.GetAsync(cacheKey, () =>
        {
            var query = from p in _repository.Table
                        join pm in _productManufacturerRepository.Table on p.Id equals pm.ProductId
                        where p.Published &&
                              !p.Deleted &&
                              manufacturerId == pm.ManufacturerId
                        select p;

            manufacturerProducts = query.ToList();

            return manufacturerProducts.Select(p => p.Id).ToList();
        });

        if (manufacturerProducts.Count == 0 && manufacturerProductIds.Count > 0)
            manufacturerProducts = await _repository.GetByIdsAsync(manufacturerProductIds, cache => default, false);

        return manufacturerProducts;
    }

    private async void UpdateProductPrices(IEnumerable<Product> products, bool increasePrices, decimal percentage)
    {
        foreach (var product in products)
        {
            product.OldPrice = product.Price;
            product.Price = increasePrices ?
                            product.Price * (1 + (percentage / 100)) :
                            product.Price * (1 - (percentage / 100));
        }
        await _repository.UpdateAsync(products.ToList());
    }
    #endregion

}
