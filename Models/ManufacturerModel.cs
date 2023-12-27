using Nop.Web.Framework.Models;

namespace Nop.Plugin.Mesut.ProductPriceManager.Models;

public record ManufacturerModel : BaseNopEntityModel
{
    public string Name { get; set; }
}
