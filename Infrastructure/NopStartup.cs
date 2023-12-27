using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Mesut.ProductPriceManager.Services;
using Nop.Plugin.Mesut.ProductPriceManager.Services.Abstract;

namespace Nop.Plugin.Mesut.ProductPriceManager.Infrastructure;

public class NopStartup : INopStartup
{
    public void Configure(IApplicationBuilder application)
    {
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductPriceService,ProductPriceService>();
    }

    public int Order => 3000;
}
