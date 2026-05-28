using Nop.Core;
using Nop.Data;
using Nop.Plugin.Product.TechnicalSheet.Domain;

namespace Nop.Plugin.Product.TechnicalSheet.Services;

/// <summary>
/// Service per le operazioni di accesso dati delle schede tecniche prodotto.
/// Utilizza il repository generico di nopCommerce per interagire con il database.
/// </summary>
public class ProductTechnicalSheetService : IProductTechnicalSheetService
{
    private readonly IRepository<ProductTechnicalSheet> _productTechnicalSheetRepository;

    /// <summary>
    /// Costruttore con dependency injection del repository
    /// </summary>
    /// <param name="productTechnicalSheetRepository">Repository generico per l'entity ProductTechnicalSheet</param>
    public ProductTechnicalSheetService(
        IRepository<ProductTechnicalSheet> productTechnicalSheetRepository)
    {
        _productTechnicalSheetRepository = productTechnicalSheetRepository;
    }

    /// <summary>
    /// Recupera una lista paginata di schede tecniche filtrate per prodotto.
    /// La paginazione avviene direttamente sul database tramite il repository, ottimizzando il caricamento dei dati.
    /// </summary>
    /// <param name="productId">Identificativo del prodotto</param>
    /// <param name="pageIndex">Indice di pagina (base 0)</param>
    /// <param name="pageSize">Elementi per pagina</param>
    /// <returns>Lista paginata di schede tecniche</returns>
    public async Task<IPagedList<ProductTechnicalSheet>> GetPagedByProductIdAsync(int productId, int pageIndex, int pageSize)
    {
        return await _productTechnicalSheetRepository.GetAllPagedAsync(
            query => query.Where(x => x.ProductId == productId),
            pageIndex,
            pageSize);
    }

    /// <summary>
    /// Recupera una singola scheda tecnica tramite il suo identificativo
    /// </summary>
    /// <param name="id">Identificativo della scheda tecnica</param>
    /// <returns>Entity della scheda tecnica o null se non trovata</returns>
    public async Task<ProductTechnicalSheet> GetByIdAsync(int id)
    {
        return await _productTechnicalSheetRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Recupera tutte le schede tecniche associate a un prodotto senza paginazione
    /// </summary>
    /// <param name="productId">Identificativo del prodotto</param>
    /// <returns>Lista completa di schede tecniche</returns>
    public async Task<IList<ProductTechnicalSheet>> GetByProductIdAsync(int productId)
    {
        return await _productTechnicalSheetRepository
            .GetAllAsync(query => query.Where(x => x.ProductId == productId));
    }

    /// <summary>
    /// Inserisce una nuova scheda tecnica nel database
    /// </summary>
    /// <param name="sheet">Entity della scheda tecnica da inserire</param>
    public async Task InsertAsync(ProductTechnicalSheet sheet)
    {
        await _productTechnicalSheetRepository.InsertAsync(sheet);
    }

    /// <summary>
    /// Aggiorna una scheda tecnica esistente nel database
    /// </summary>
    /// <param name="sheet">Entity della scheda tecnica con le modifiche</param>
    public async Task UpdateAsync(ProductTechnicalSheet sheet)
    {
        await _productTechnicalSheetRepository.UpdateAsync(sheet);
    }

    /// <summary>
    /// Elimina una scheda tecnica dal database
    /// </summary>
    /// <param name="sheet">Entity della scheda tecnica da eliminare</param>
    public async Task DeleteAsync(ProductTechnicalSheet sheet)
    {
        await _productTechnicalSheetRepository.DeleteAsync(sheet);
    }
}
