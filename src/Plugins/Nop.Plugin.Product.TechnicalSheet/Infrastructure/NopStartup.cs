using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Product.TechnicalSheet.Services;

namespace Nop.Plugin.Product.TechnicalSheet.Infrastructure;

public class NopStartup : INopStartup
{
    /// <summary>
    /// Quando qualcuno richiede IProductTechnicalSheetService, usa ProductTechnicalSheetService
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductTechnicalSheetService, ProductTechnicalSheetService>();
    }
        public void Configure(IApplicationBuilder application)
    {
    }
    public int Order => 1000;
}

