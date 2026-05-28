using System;
using Nop.Core;

namespace Nop.Plugin.Product.TechnicalSheet.Domain;

/// <summary>
/// Entity che rappresenta una scheda tecnica associata a un prodotto nel database
/// </summary>
public class ProductTechnicalSheet : BaseEntity 
{
    /// <summary>
    /// Identificativo del prodotto a cui la scheda tecnica appartiene
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Titolo della scheda tecnica
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrizione dettagliata del contenuto tecnico
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Codice tecnico identificativo (es. numero parte, codice fornitore, riferimento disegno)
    /// </summary>
    public string TechnicalCode { get; set; } = string.Empty;

    /// <summary>
    /// Data e ora UTC di creazione della scheda tecnica
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }

}
