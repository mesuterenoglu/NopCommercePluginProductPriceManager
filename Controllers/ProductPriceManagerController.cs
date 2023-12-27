using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Mesut.ProductPriceManager.Models;
using Nop.Plugin.Mesut.ProductPriceManager.Services.Abstract;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Mesut.ProductPriceManager.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.Admin)]
[AutoValidateAntiforgeryToken]
public class ProductPriceManagerController : BasePluginController
{
    #region Fields
    private readonly IProductPriceService _productPriceService;
    private readonly INotificationService _notificationService;
    private readonly ILocalizationService _localizationService;
    #endregion

    #region Ctor
    public ProductPriceManagerController(IProductPriceService productPriceService, INotificationService notificationService, ILocalizationService localizationService)
    {
        _productPriceService = productPriceService;
        _notificationService = notificationService;
        _localizationService = localizationService;
    }
    #endregion

    #region Methods

    public IActionResult Configure()
    {
        return View("~/Plugins/Mesut.ProductPriceManager/Views/Configure.cshtml", new ConfigurationModel());
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        await _productPriceService.UpdateProductPricesByManufacturersAsync(
                                    model.SelectedManufacturers,
                                    model.PriceAction == "IncreasePrices",
                                    model.Percentage);

        await _productPriceService.UpdateProductPricesByCategoriesAsync(
                                    model.SelectedCategories,
                                    model.PriceAction == "IncreasePrices",
                                    model.Percentage);
        _notificationService.SuccessNotification("Product prices updated succesfully");

        return View("~/Plugins/Mesut.ProductPriceManager/Views/Configure.cshtml", model);
    }
    #endregion
}
