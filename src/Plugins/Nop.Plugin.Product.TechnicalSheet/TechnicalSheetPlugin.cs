using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Plugin.Product.TechnicalSheet.Infrastructure;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Product.TechnicalSheet;

/// <summary>
/// Plugin principale per la gestione delle schede tecniche prodotto.
/// Implementa IWidgetPlugin per l'integrazione nella pagina prodotto (tab aggiuntivo).
/// Gestisce l'installazione/disinstallazione delle risorse localizzate.
/// </summary>
public class TechnicalSheetPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ILanguageService _languageService;

    /// <summary>
    /// Costruttore con dependency injection
    /// </summary>
    /// <param name="localizationService">Service per la gestione delle risorse localizzate</param>
    /// <param name="languageService">Service per la gestione delle lingue disponibili</param>
    public TechnicalSheetPlugin(
        ILocalizationService localizationService,
        ILanguageService languageService)
    {
        _localizationService = localizationService;
        _languageService = languageService;
    }

    /// <summary>
    /// Indica se il widget deve essere nascosto dalla lista widget globale.
    /// true perchè il plugin si aggancia automaticamente alla zona AdminWidgetZones.ProductDetailsBlock.
    /// </summary>
    public bool HideInWidgetList => true;

    /// <summary>
    /// Restituisce le widget zone in cui il plugin deve apparire.
    /// Il plugin si aggancia alla zona ProductDetailsBlock nella pagina di modifica prodotto.
    /// </summary>
    /// <returns>Lista contenente AdminWidgetZones.ProductDetailsBlock</returns>
    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string>
        {
            AdminWidgetZones.ProductDetailsBlock
        });
    }


    /// <summary>
    /// Restituisce il nome del ViewComponent da renderizzare per una specifica widget zone
    /// </summary>
    /// <param name="widgetZone">Nome della widget zone</param>
    /// <returns>Nome del ViewComponent o null se la zona non è gestita</returns>
    public string? GetWidgetViewComponentName(string widgetZone)
    {
         if (widgetZone == AdminWidgetZones.ProductDetailsBlock)
        return TechnicalSheetDefaults.AdminViewComponentName;

        return null;
    }

    /// <summary>
    /// Installazione del plugin. Registra tutte le risorse localizzate in inglese (per tutte le lingue)
    /// e in italiano (se la lingua italiana è presente nel sistema).
    /// </summary>
    public override async Task InstallAsync()
    {
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Admin.ProductTechnicalSheet.Fields.Title"] = "Title",
            ["Admin.ProductTechnicalSheet.Fields.Description"] = "Description",
            ["Admin.ProductTechnicalSheet.Fields.TechnicalCode"] = "Technical Code",
            ["Admin.ProductTechnicalSheet.Fields.ProductId"] = "Product",
            ["Admin.ProductTechnicalSheet.AddNew"] = "Add new",
            ["Admin.ProductTechnicalSheet.Edit"] = "Edit",
            ["Admin.ProductTechnicalSheet.Delete"] = "Delete",
            ["Admin.ProductTechnicalSheet.BackToList"] = "Back to list",
            ["Admin.ProductTechnicalSheet.Save"] = "Save",
            ["Admin.ProductTechnicalSheet.List"] = "Product Technical Sheets",
            ["Admin.ProductTechnicalSheet.Create"] = "Create Technical Sheet",
            ["Admin.ProductTechnicalSheet.EditPage"] = "Edit Technical Sheet",
            ["Admin.ProductTechnicalSheet.NoRecords"] = "No records found",
            ["Admin.ProductTechnicalSheet.Created"] = "Created successfully",
            ["Admin.ProductTechnicalSheet.Updated"] = "Updated successfully",
            ["Admin.ProductTechnicalSheet.Deleted"] = "Deleted successfully",
            ["Admin.ProductTechnicalSheet.NotFound"] = "Technical sheet not found",
            ["Admin.ProductTechnicalSheet.AccessDenied"] = "Access denied",
            ["Admin.ProductTechnicalSheet.ValidationError"] = "Please correct the validation errors",
            ["Admin.ProductTechnicalSheet.Menu"] = "Technical Sheets",
        });

        var languages = await _languageService.GetAllLanguagesAsync(true);
        var italianLanguage = languages.FirstOrDefault(l => l.LanguageCulture.StartsWith("it", System.StringComparison.InvariantCultureIgnoreCase));

        if (italianLanguage != null)
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Admin.ProductTechnicalSheet.Fields.Title"] = "Titolo",
                ["Admin.ProductTechnicalSheet.Fields.Description"] = "Descrizione",
                ["Admin.ProductTechnicalSheet.Fields.TechnicalCode"] = "Codice Tecnico",
                ["Admin.ProductTechnicalSheet.Fields.ProductId"] = "Prodotto",
                ["Admin.ProductTechnicalSheet.AddNew"] = "Aggiungi nuovo",
                ["Admin.ProductTechnicalSheet.Edit"] = "Modifica",
                ["Admin.ProductTechnicalSheet.Delete"] = "Elimina",
                ["Admin.ProductTechnicalSheet.BackToList"] = "Torna alla lista",
                ["Admin.ProductTechnicalSheet.Save"] = "Salva",
                ["Admin.ProductTechnicalSheet.List"] = "Schede Tecniche Prodotto",
                ["Admin.ProductTechnicalSheet.Create"] = "Crea Scheda Tecnica",
                ["Admin.ProductTechnicalSheet.EditPage"] = "Modifica Scheda Tecnica",
                ["Admin.ProductTechnicalSheet.NoRecords"] = "Nessun dato disponibile",
                ["Admin.ProductTechnicalSheet.Created"] = "Creazione avvenuta con successo",
                ["Admin.ProductTechnicalSheet.Updated"] = "Aggiornamento avvenuto con successo",
                ["Admin.ProductTechnicalSheet.Deleted"] = "Eliminazione avvenuta con successo",
                ["Admin.ProductTechnicalSheet.NotFound"] = "Scheda tecnica non trovata",
                ["Admin.ProductTechnicalSheet.AccessDenied"] = "Accesso negato",
                ["Admin.ProductTechnicalSheet.ValidationError"] = "Correggi gli errori di validazione",
                ["Admin.ProductTechnicalSheet.Menu"] = "Schede Tecniche",
            }, italianLanguage.Id);
        }

        await base.InstallAsync();
    }

    /// <summary>
    /// Disinstallazione del plugin. Rimuove tutte le risorse localizzate con prefisso "Admin.ProductTechnicalSheet".
    /// </summary>
    public override async Task UninstallAsync()
    {
        await _localizationService.DeleteLocaleResourcesAsync("Admin.ProductTechnicalSheet");

        await base.UninstallAsync();
    }
}
