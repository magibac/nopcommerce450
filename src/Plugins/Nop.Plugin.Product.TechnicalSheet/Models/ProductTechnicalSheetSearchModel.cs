using Nop.Web.Framework.Models;

namespace Nop.Plugin.Product.TechnicalSheet.Models;

/// <summary>
/// Modello per la ricerca paginata delle schede tecniche. Utilizzato dalla griglia DataTables.
/// </summary>
public record ProductTechnicalSheetSearchModel : BaseSearchModel
{
    /// <summary>
    /// Filtro per identificativo prodotto. Viene popolato dalla route durante la richiesta GET e inviato alla action POST per la griglia.
    /// </summary>
    public int ProductId { get; set; }
}
