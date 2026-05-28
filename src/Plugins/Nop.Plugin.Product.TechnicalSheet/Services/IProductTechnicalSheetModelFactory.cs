using Nop.Plugin.Product.TechnicalSheet.Models;

namespace Nop.Plugin.Product.TechnicalSheet.Services;

/// <summary>
/// Interfaccia per la factory che prepara i modelli per la griglia DataTables delle schede tecniche
/// </summary>
public interface IProductTechnicalSheetModelFactory
{
    /// <summary>
    /// Prepara il modello di ricerca con i parametri di paginazione predefiniti
    /// </summary>
    /// <param name="searchModel">Modello di ricerca da inizializzare</param>
    /// <param name="productId">Identificativo del prodotto per filtrare le schede</param>
    /// <returns>Modello di ricerca configurato</returns>
    Task<ProductTechnicalSheetSearchModel> PrepareSearchModelAsync(ProductTechnicalSheetSearchModel searchModel, int productId);

    /// <summary>
    /// Prepara il modello di risposta paginato per la griglia DataTables
    /// </summary>
    /// <param name="searchModel">Modello di ricerca con i parametri di paginazione correnti</param>
    /// <returns>Modello contenente la lista paginata e i metadati DataTables</returns>
    Task<ProductTechnicalSheetListModel> PrepareListModelAsync(ProductTechnicalSheetSearchModel searchModel);
}
