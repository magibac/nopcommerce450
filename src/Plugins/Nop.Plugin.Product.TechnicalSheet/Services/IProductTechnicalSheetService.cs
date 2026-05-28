using Nop.Plugin.Product.TechnicalSheet.Domain;

namespace Nop.Plugin.Product.TechnicalSheet.Services;

public interface IProductTechnicalSheetService
{
    /// <summary>
    /// Ottieni liste tecniche per productId
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<IList<ProductTechnicalSheet>> GetByProductIdAsync(int productId);

    /// <summary>
    /// Ottieni scheda tecnica per id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProductTechnicalSheet> GetByIdAsync(int id);

    /// <summary>
    /// Inserimento Scheda
    /// </summary>
    /// <param name="sheet"></param>
    /// <returns></returns>
    Task InsertAsync(ProductTechnicalSheet sheet);

    /// <summary>
    /// Aggionamento Scheda
    /// </summary>
    /// <param name="sheet"></param>
    /// <returns></returns>
    Task UpdateAsync(ProductTechnicalSheet sheet);

    /// <summary>
    /// Elimina Scheda
    /// </summary>
    /// <param name="sheet"></param>
    /// <returns></returns>
    Task DeleteAsync(ProductTechnicalSheet sheet);


}
