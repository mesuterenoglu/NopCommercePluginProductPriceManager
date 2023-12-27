using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Mesut.ProductPriceManager;

public class ProductPriceManagerBase : BasePlugin
{
    #region Fields
    private readonly IWebHelper _webHelper;
    private readonly ILocalizationService _localizationService;

    #endregion

    #region Ctor
    public ProductPriceManagerBase(IWebHelper webHelper, ILocalizationService localizationService)
    {
        _webHelper = webHelper;
        _localizationService = localizationService;
    }
    #endregion

    #region Methods
    public override async Task InstallAsync()
    {
        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Mesut.ProductPriceManager.Fields.PriceAction.Required"] =
                "Price Action needs to be selected",
            ["Plugins.Mesut.ProductPriceManager.Fields.PriceAction.IsNotValid"] =
                "Price Action value has to be valid.",
            ["Plugins.Mesut.ProductPriceManager.Fields.Percentage.Required"] =
                "Percentage has to be defined.",
            ["Plugins.Mesut.ProductPriceManager.Fields.Percentage.IsNotValid"] =
                "Percentage value has be greater than 0. Max percentage value is 999.",
            ["Plugins.Mesut.ProductPriceManager.Fields.SelectedManufacturers"] = "Select Manufacturer",
            ["Plugins.Mesut.ProductPriceManager.Fields.SelectedManufacturers.Hint"] = 
                "You can select more than once manufacturer.",

            ["Plugins.Mesut.ProductPriceManager.Fields.SelectedCategories"] = "Select Category",
            ["Plugins.Mesut.ProductPriceManager.Fields.SelectedCategories.Hint"] =
                "You can select more than once category.",

        });

        await base.InstallAsync();
    }
    public override async Task UninstallAsync()
    {
        //locales
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Mesut.DiscountManager");

        await base.UninstallAsync();
    }
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/ProductPriceManager/Configure";
    }
    #endregion
}