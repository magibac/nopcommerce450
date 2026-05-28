using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Product.TechnicalSheet.Models;

public record ProductTechnicalSheetModel : BaseNopEntityModel
{
    public int ProductId { get; set; }

    [NopResourceDisplayName("Admin.ProductTechnicalSheet.Fields.Title")]
    public string Title { get; set; } = string.Empty;

    [NopResourceDisplayName("Admin.ProductTechnicalSheet.Fields.Description")]
    public string Description { get; set; } = string.Empty;

    [NopResourceDisplayName("Admin.ProductTechnicalSheet.Fields.TechnicalCode")]
    public string TechnicalCode { get; set; } = string.Empty;
}