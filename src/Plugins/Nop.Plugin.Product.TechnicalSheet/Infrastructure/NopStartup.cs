using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Product.TechnicalSheet.Services;

namespace Nop.Plugin.Product.TechnicalSheet.Infrastructure;

/// <summary>
/// Configurazione del plugin all'avvio dell'applicazione.
/// Registra i servizi del plugin nel container DI di nopCommerce tramite INopStartup.
/// </summary>
public class NopStartup : INopStartup
{
    /// <summary>
    /// Registra i servizi del plugin nel container di dependency injection.
    /// IProductTechnicalSheetService e IProductTechnicalSheetModelFactory vengono registrati come Scoped.
    /// </summary>
    /// <param name="services">Collezione dei servizi dell'applicazione</param>
    /// <param name="configuration">Configurazione dell'applicazione</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductTechnicalSheetService, ProductTechnicalSheetService>();
        //services.AddScoped<IProductTechnicalSheetModelFactory, ProductTechnicalSheetModelFactory>();
    }

    /// <summary>
    /// Configura la pipeline dell'applicazione. Non utilizzato dai plugin, implementato per completezza dell'interfaccia.
    /// </summary>
    /// <param name="application">Application builder per la configurazione della pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// Ordine di esecuzione del modulo. Valore 1000 per esecuzione nella fascia standard.
    /// </summary>
    public int Order => 1000;
}
