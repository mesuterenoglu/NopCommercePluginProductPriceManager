using Nop.Web.Framework.Models;

namespace Nop.Plugin.Mesut.ProductPriceManager.Models;

public record CategoryModel : BaseNopEntityModel
{
    public string Name { get; set; }
}
