using System;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Mesut.ProductPriceManager.Models;

public class ConfigurationModel
{
    [NopResourceDisplayName("Plugins.Mesut.ProductPriceManager.Fields.SelectedCategories")]
    public int[] SelectedCategories { get; set; } = Array.Empty<int>();

    [NopResourceDisplayName("Plugins.Mesut.ProductPriceManager.Fields.SelectedManufacturers")]
    public int[] SelectedManufacturers { get; set; } = Array.Empty<int>();
    public string PriceAction { get; set; }
    public decimal Percentage { get; set; }
}
