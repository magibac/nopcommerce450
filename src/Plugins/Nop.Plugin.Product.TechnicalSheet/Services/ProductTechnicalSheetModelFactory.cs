using Nop.Plugin.Product.TechnicalSheet.Models;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Product.TechnicalSheet.Services;

/// <summary>
/// Factory per la preparazione dei modelli della griglia DataTables.
/// Gestisce la paginazione lato server e il mapping dai dati del repository ai modelli della view.
/// </summary>
public class ProductTechnicalSheetModelFactory : IProductTechnicalSheetModelFactory
{
    private readonly IProductTechnicalSheetService _productTechnicalSheetService;

    /// <summary>
    /// Costruttore con dependency injection per il service delle schede tecniche
    /// </summary>
    /// <param name="productTechnicalSheetService">Service per l'accesso ai dati</param>
    public ProductTechnicalSheetModelFactory(IProductTechnicalSheetService productTechnicalSheetService)
    {
        _productTechnicalSheetService = productTechnicalSheetService;
    }

    /// <summary>
    /// Prepara il modello di ricerca inizializzando la paginazione predefinita e il filtro prodotto
    /// </summary>
    /// <param name="searchModel">Modello di ricerca da configurare</param>
    /// <param name="productId">Identificativo del prodotto per il filtro</param>
    /// <returns>Modello di ricerca pronto per la view</returns>
    public virtual Task<ProductTechnicalSheetSearchModel> PrepareSearchModelAsync(
        ProductTechnicalSheetSearchModel searchModel, int productId)
    {
        searchModel.ProductId = productId;
        searchModel.SetGridPageSize();

        return Task.FromResult(searchModel);
    }

    /// <summary>
    /// Prepara il modello di risposta paginato per la griglia DataTables.
    /// Recupera i dati paginati dal database e li mappa sui modelli della view.
    /// </summary>
    /// <param name="searchModel">Modello di ricerca con i parametri di paginazione</param>
    /// <returns>Modello contenente i dati paginati e i metadati per DataTables</returns>
    public virtual async Task<ProductTechnicalSheetListModel> PrepareListModelAsync(
        ProductTechnicalSheetSearchModel searchModel)
    {
        var pagedSheets = await _productTechnicalSheetService.GetPagedByProductIdAsync(
            searchModel.ProductId, searchModel.Page - 1, searchModel.PageSize);

        var model = new ProductTechnicalSheetListModel().PrepareToGrid(searchModel, pagedSheets, () =>
        {
            return pagedSheets.Select(sheet => new ProductTechnicalSheetModel
            {
                Id = sheet.Id,
                ProductId = sheet.ProductId,
                Title = sheet.Title,
                Description = sheet.Description,
                TechnicalCode = sheet.TechnicalCode
            });
        });

        return model;
    }
}
