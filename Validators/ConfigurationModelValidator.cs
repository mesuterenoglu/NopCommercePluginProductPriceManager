using FluentValidation;
using Nop.Plugin.Mesut.ProductPriceManager.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Mesut.ProductPriceManager.Validators;

public class ConfigurationModelValidator : BaseNopValidator<ConfigurationModel>
{
    public ConfigurationModelValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.PriceAction)
            .NotNull()
            .WithMessageAwait(localizationService
                .GetResourceAsync("Plugins.Mesut.ProductPriceManager.Fields.PriceAction.Required"))
            .Must((configuration, priceAction) =>
            {
                return string.Equals("IncreasePrices", priceAction) ||
                        string.Equals("ReducePrices", priceAction);
            })
            .WithMessageAwait(localizationService
                .GetResourceAsync("Plugins.Mesut.ProductPriceManager.Fields.PriceAction.IsNotValid"));

        RuleFor(x => x.Percentage)
            .NotNull()
            .WithMessageAwait(localizationService
                .GetResourceAsync("Plugins.Mesut.ProductPriceManager.Fields.Percentage.Required"))
            .Must((configuration, percentage) =>
            {
                return 0 < percentage && percentage <= 999;
            })
            .WithMessageAwait(localizationService
                .GetResourceAsync("Plugins.Mesut.ProductPriceManager.Fields.Percentage.IsNotValid"));

    }
}
