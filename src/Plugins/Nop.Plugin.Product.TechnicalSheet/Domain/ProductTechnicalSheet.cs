using System;
using Nop.Core;

namespace Nop.Plugin.Product.TechnicalSheet.Domain;

public class ProductTechnicalSheet : BaseEntity 
{
    public int ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TechnicalCode { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }

}
