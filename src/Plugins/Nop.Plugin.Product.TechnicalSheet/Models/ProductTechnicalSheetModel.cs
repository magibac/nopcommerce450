using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Product.TechnicalSheet.Models;

/// <summary>
/// Modello dati per il form di creazione/modifica scheda tecnica e per la griglia DataTables
/// </summary>
public record ProductTechnicalSheetModel : BaseNopEntityModel
{
    /// <summary>
    /// Identificativo del prodotto a cui la scheda tecnica è associata
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Titolo della scheda tecnica. Campo obbligatorio.
    /// </summary>
    [NopResourceDisplayName("Admin.ProductTechnicalSheet.Fields.Title")]
    [Required]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrizione dettagliata della scheda tecnica
    /// </summary>
    [NopResourceDisplayName("Admin.ProductTechnicalSheet.Fields.Description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Codice tecnico identificativo della scheda (es. numero disegno, codice fornitore)
    /// </summary>
    [NopResourceDisplayName("Admin.ProductTechnicalSheet.Fields.TechnicalCode")]
    public string TechnicalCode { get; set; } = string.Empty;
}
