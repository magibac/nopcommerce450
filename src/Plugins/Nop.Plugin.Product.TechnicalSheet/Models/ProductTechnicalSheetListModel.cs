using Nop.Web.Framework.Models;

namespace Nop.Plugin.Product.TechnicalSheet.Models;

/// <summary>
/// Modello contenitore per la risposta JSON della griglia DataTables.
/// Incapsula la lista paginata di schede tecniche con i metadati di paginazione (Draw, RecordsTotal, RecordsFiltered).
/// </summary>
public record ProductTechnicalSheetListModel : BasePagedListModel<ProductTechnicalSheetModel>
{
}
