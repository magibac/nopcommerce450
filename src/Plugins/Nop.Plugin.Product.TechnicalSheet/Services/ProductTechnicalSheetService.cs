using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Product.TechnicalSheet.Domain;

namespace Nop.Plugin.Product.TechnicalSheet.Services;


public class ProductTechnicalSheetService : IProductTechnicalSheetService
{
    /// <summary>
    /// Repository per accesso ai dati delle schede tecniche prodotto
    /// </summary>
    private readonly IRepository<ProductTechnicalSheet> _productTechnicalSheetRepository;

    /// <summary>
    /// Iniezione della dipendenza IRepository tramite Dependency Injection
    /// </summary>
    public ProductTechnicalSheetService(
        IRepository<ProductTechnicalSheet> productTechnicalSheetRepository)
    {
        _productTechnicalSheetRepository = productTechnicalSheetRepository;
    }

    /// <summary>
    /// Recupera dal database tutte le schede tecniche associate a un prodotto
    /// </summary>
    /// <param name="productId">Identificativo del prodotto</param>
    /// <returns>Lista delle schede tecniche del prodotto</returns>
     public async Task<ProductTechnicalSheet> GetByIdAsync(int id)
    {
        return await _productTechnicalSheetRepository.GetByIdAsync(id);
    }
    public async Task<IList<ProductTechnicalSheet>> GetByProductIdAsync(int productId)
    {
        return await _productTechnicalSheetRepository.GetAllAsync(query => query.Where(x => x.ProductId == productId));
    }
    public async Task InsertAsync(ProductTechnicalSheet sheet)
    {
        await _productTechnicalSheetRepository.InsertAsync(sheet);
    }
    public async Task UpdateAsync(ProductTechnicalSheet sheet)
    {
        await _productTechnicalSheetRepository.UpdateAsync(sheet);
    }
    public async Task DeleteAsync(ProductTechnicalSheet sheet)
    {
        await _productTechnicalSheetRepository.DeleteAsync(sheet);
    }
   
}