using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Product.TechnicalSheet;

public class TechnicalSheetPlugin : BasePlugin
{
    private readonly ILocalizationService _localizationService;

    public TechnicalSheetPlugin(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public override async Task InstallAsync()
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
        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _localizationService.DeleteLocaleResourcesAsync("Admin.ProductTechnicalSheet");

        await base.UninstallAsync();
    }
}
