using Nop.Core;
using Nop.Plugin.Product.TechnicalSheet.Domain;

namespace Nop.Plugin.Product.TechnicalSheet.Services;

/// <summary>
/// Interfaccia del service per la gestione delle schede tecniche prodotto.
/// Definisce le operazioni di accesso dati disponibili per il layer controller e factory.
/// </summary>
public interface IProductTechnicalSheetService
{
    /// <summary>
    /// Recupera tutte le schede tecniche associate a un prodotto
    /// </summary>
    /// <param name="productId">Identificativo del prodotto</param>
    /// <returns>Lista completa di schede tecniche (non paginata)</returns>
    Task<IList<ProductTechnicalSheet>> GetByProductIdAsync(int productId);

    /// <summary>
    /// Recupera una singola scheda tecnica per identificativo
    /// </summary>
    /// <param name="id">Identificativo della scheda tecnica</param>
    /// <returns>Entity della scheda tecnica o null se non trovata</returns>
    Task<ProductTechnicalSheet> GetByIdAsync(int id);

    /// <summary>
    /// Inserisce una nuova scheda tecnica nel database
    /// </summary>
    /// <param name="sheet">Entity della scheda tecnica da inserire</param>
    Task InsertAsync(ProductTechnicalSheet sheet);

    /// <summary>
    /// Aggiorna una scheda tecnica esistente nel database
    /// </summary>
    /// <param name="sheet">Entity della scheda tecnica con i dati aggiornati</param>
    Task UpdateAsync(ProductTechnicalSheet sheet);

    /// <summary>
    /// Elimina una scheda tecnica dal database
    /// </summary>
    /// <param name="sheet">Entity della scheda tecnica da eliminare</param>
    Task DeleteAsync(ProductTechnicalSheet sheet);
}
