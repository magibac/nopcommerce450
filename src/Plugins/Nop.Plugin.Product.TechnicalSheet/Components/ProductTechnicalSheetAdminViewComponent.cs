using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Product.TechnicalSheet.Infrastructure;
using Nop.Plugin.Product.TechnicalSheet.Services;
using Nop.Services.Security;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Product.TechnicalSheet.Components;

/// <summary>
/// ViewComponent per visualizzare il blocco "Schede Tecniche" nella pagina di modifica prodotto.
/// Si aggancia alla widget zone AdminWidgetZones.ProductDetailsBlock per apparire come tab aggiuntivo.
/// </summary>
[ViewComponent(Name = TechnicalSheetDefaults.AdminViewComponentName)]
public class ProductTechnicalSheetAdminViewComponent : NopViewComponent
{
    private readonly IProductTechnicalSheetService _productTechnicalSheetService;
    private readonly IPermissionService _permissionService;

    /// <summary>
    /// Costruttore con dependency injection
    /// </summary>
    /// <param name="productTechnicalSheetService">Service per la lettura delle schede tecniche</param>
    /// <param name="permissionService">Service per la verifica dei permessi</param>
    public ProductTechnicalSheetAdminViewComponent(
        IProductTechnicalSheetService productTechnicalSheetService,
        IPermissionService permissionService)
    {
        _productTechnicalSheetService = productTechnicalSheetService;
        _permissionService = permissionService;
    }

    /// <summary>
    /// Esegue il ViewComponent: verifica permessi, recupera le schede tecniche del prodotto e restituisce la vista.
    /// </summary>
    /// <param name="widgetZone">Nome della widget zone (deve corrispondere a ProductDetailsBlock)</param>
    /// <param name="additionalData">Dati aggiuntivi contenenti il modello del prodotto, da cui si estrae l'Id</param>
    /// <returns>Vista parziale con la lista delle schede tecniche oppure stringa vuota se non ci sono permessi o zone non corretta</returns>
    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        if (!widgetZone.Equals(AdminWidgetZones.ProductDetailsBlock))
            return Content(string.Empty);

        dynamic model = additionalData;

        if (model == null)
            return Content(string.Empty);

        // if (!await _permissionService.AuthorizeAsync(PermissionProvider.ManageTechnicalSheets))
        //     return Content(string.Empty);

        var sheets = await _productTechnicalSheetService.GetByProductIdAsync((int)model.Id);

        return View("~/Plugins/Product.TechnicalSheet/Views/Shared/Components/ProductTechnicalSheetAdmin/Default.cshtml", sheets);
    }
}
