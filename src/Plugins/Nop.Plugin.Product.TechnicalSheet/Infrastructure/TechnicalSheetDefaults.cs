namespace Nop.Plugin.Product.TechnicalSheet.Infrastructure;

/// <summary>
/// Costanti del plugin Product.TechnicalSheet.
/// Contiene i nomi utilizzati per l'identificazione del plugin, dei componenti e delle viste.
/// </summary>
public static class TechnicalSheetDefaults
{
    /// <summary>
    /// Nome univoco del plugin. Utilizzato per il percorso di output delle view e per l'identificazione nel sistema.
    /// </summary>
    public const string SystemName = "Product.TechnicalSheet";

    /// <summary>
    /// Nome del ViewComponent utilizzato per il rendering del tab nella pagina di modifica prodotto.
    /// Deve corrispondere al nome registrato nell'attributo [ViewComponent].
    /// </summary>
    public const string AdminViewComponentName = "ProductTechnicalSheetAdmin";
}
