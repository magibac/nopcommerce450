using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Product.TechnicalSheet.Domain;
using Nop.Plugin.Product.TechnicalSheet.Models;
using Nop.Plugin.Product.TechnicalSheet.Services;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Product.TechnicalSheet.Controllers;

/// <summary>
/// Controller per la gestione delle schede tecniche prodotto nel contesto della pagina Product Edit.
/// Gestisce le operazioni CRUD e la registrazione audit tramite logger.
/// </summary>
[AuthorizeAdmin]
[Area(AreaNames.Admin)]
[AutoValidateAntiforgeryToken]
public class ProductTechnicalSheetController : BasePluginController
{
    private readonly IProductTechnicalSheetService _productTechnicalSheetService;
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly ILogger _logger;

    /// <summary>
    /// Costruttore con dependency injection. Riceve tutti i servizi necessari dal container DI di nopCommerce.
    /// </summary>
    /// <param name="productTechnicalSheetService">Service per le operazioni CRUD sulle schede tecniche</param>
    /// <param name="localizationService">Service per il recupero delle risorse localizzate</param>
    /// <param name="notificationService">Service per le notifiche toast nell'interfaccia admin</param>
    /// <param name="logger">Service per la registrazione degli eventi di audit</param>
    public ProductTechnicalSheetController(
        IProductTechnicalSheetService productTechnicalSheetService,
        ILocalizationService localizationService,
        INotificationService notificationService,
        ILogger logger)
    {
        _productTechnicalSheetService = productTechnicalSheetService;
        _localizationService = localizationService;
        _notificationService = notificationService;
        _logger = logger;
    }

    /// <summary>
    /// Elimina una scheda tecnica tramite chiamata AJAX. Registra l'evento nel log di sistema.
    /// </summary>
    /// <param name="id">Identificativo della scheda tecnica da eliminare</param>
    /// <returns>JSON con indicazione di successo o fallimento e messaggio localizzato</returns>
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        //if (!await _permissionService.AuthorizeAsync(PermissionProvider.ManageTechnicalSheets))
        //    return Json(new { success = false, message = await _localizationService.GetResourceAsync("Admin.ProductTechnicalSheet.AccessDenied") });

        var entity = await _productTechnicalSheetService.GetByIdAsync(id);

        if (entity == null)
            return Json(new
            {
                success = false,
                message = await _localizationService.GetResourceAsync("Admin.ProductTechnicalSheet.NotFound")
            });

        await _productTechnicalSheetService.DeleteAsync(entity);

        await _logger.InformationAsync($"Scheda tecnica eliminata: Id={id}, Titolo={entity.Title}");

        return Json(new { success = true });
    }

    /// <summary>
    /// Visualizza il form di creazione o modifica di una scheda tecnica.
    /// In modalità modifica (id > 0) carica i dati esistenti dal database e li mappa sul modello.
    /// In modalità creazione (id = 0) inizializza un nuovo modello vuoto con il ProductId preimpostato.
    /// </summary>
    /// <param name="productId">Identificativo del prodotto associato</param>
    /// <param name="id">Identificativo della scheda tecnica (0 per creazione, >0 per modifica)</param>
    /// <returns>Vista con il modello del form</returns>
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int productId, int id = 0)
    {
        //if (!await _permissionService.AuthorizeAsync(PermissionProvider.ManageTechnicalSheets))
        //    return AccessDeniedView();

        if (id > 0)
        {
            var entity = await _productTechnicalSheetService.GetByIdAsync(id);

            if (entity == null)
            {
                await _logger.WarningAsync($"Scheda tecnica non trovata: Id={id}");
                return NotFound();
            }

            var model = new ProductTechnicalSheetModel
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Title = entity.Title,
                Description = entity.Description,
                TechnicalCode = entity.TechnicalCode
            };

            return View("~/Plugins/Product.TechnicalSheet/Views/CreateOrEdit.cshtml", model);
        }

        var newModel = new ProductTechnicalSheetModel
        {
            ProductId = productId
        };

        return View("~/Plugins/Product.TechnicalSheet/Views/CreateOrEdit.cshtml", newModel);
    }

    /// <summary>
    /// Gestisce il salvataggio del form di creazione/modifica.
    /// Valida il modello, esegue l'operazione CRUD appropriata (insert o update),
    /// registra l'evento nel log e mostra una notifica di successo all'utente.
    /// In caso di errori di validazione, mostra la notifica di errore e ripresenta il form.
    /// </summary>
    /// <param name="model">Modello con i dati del form inviati dall'utente</param>
    /// <returns>Redirect alla lista delle schede tecniche del prodotto in caso di successo, altrimenti ripresenta il form con errori</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(ProductTechnicalSheetModel model)
    {
        //if (!await _permissionService.AuthorizeAsync(PermissionProvider.ManageTechnicalSheets))
        //    return AccessDeniedView();

        if (!ModelState.IsValid)
        {
            _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("Admin.ProductTechnicalSheet.ValidationError"));
            return View("~/Plugins/Product.TechnicalSheet/Views/CreateOrEdit.cshtml", model);
        }

        if (model.Id > 0)
        {
            var entity = await _productTechnicalSheetService.GetByIdAsync(model.Id);

            if (entity == null)
                return NotFound();

            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.TechnicalCode = model.TechnicalCode;
            entity.ProductId = model.ProductId;

            await _productTechnicalSheetService.UpdateAsync(entity);

            await _logger.InformationAsync($"Scheda tecnica aggiornata: Id={entity.Id}, Titolo={entity.Title}, ProductId={entity.ProductId}");

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.ProductTechnicalSheet.Updated"));
        }
        else
        {
            var entity = new ProductTechnicalSheet
            {
                ProductId = model.ProductId,
                Title = model.Title,
                Description = model.Description,
                TechnicalCode = model.TechnicalCode,
                CreatedOnUtc = DateTime.UtcNow
            };

            await _productTechnicalSheetService.InsertAsync(entity);

            await _logger.InformationAsync($"Scheda tecnica creata: Id={entity.Id}, Titolo={entity.Title}, ProductId={entity.ProductId}");

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.ProductTechnicalSheet.Created"));
        }

        return RedirectToAction("Edit", "Product", new { id = model.ProductId, area = AreaNames.Admin });
    }
}
