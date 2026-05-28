using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Product.TechnicalSheet.Domain;
using Nop.Plugin.Product.TechnicalSheet.Models;
using Nop.Plugin.Product.TechnicalSheet.Services;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Product.TechnicalSheet.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.Admin)]
[AutoValidateAntiforgeryToken]
public class ProductTechnicalSheetController : BasePluginController
{
    private readonly IProductTechnicalSheetService _productTechnicalSheetService;
    private readonly ILocalizationService _localizationService;
    private readonly IPermissionService _permissionService;

    public ProductTechnicalSheetController(
        IProductTechnicalSheetService productTechnicalSheetService,
        ILocalizationService localizationService,
        IPermissionService permissionService)
    {
        _productTechnicalSheetService = productTechnicalSheetService;
        _localizationService = localizationService;
        _permissionService = permissionService;
    }

    [HttpGet]
    public async Task<IActionResult> List(int productId)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
            return AccessDeniedView();

        var sheets = await _productTechnicalSheetService.GetByProductIdAsync(productId);

        ViewBag.ProductId = productId;

        //return View(sheets);
        return View("~/Plugins/Product.TechnicalSheet/Views/List.cshtml", sheets);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
            return Json(new { success = false, message = "Access denied" });

        var entity = await _productTechnicalSheetService.GetByIdAsync(id);

        if (entity == null)
            return Json(new { success = false, message = await _localizationService.GetResourceAsync("Admin.ProductTechnicalSheet.NotFound") });

        await _productTechnicalSheetService.DeleteAsync(entity);

        return Json(new { success = true });
    }

    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(int productId, int id = 0)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
            return AccessDeniedView();

        if (id > 0)
        {
            var entity = await _productTechnicalSheetService.GetByIdAsync(id);

            if (entity == null)
                return NotFound();

            var model = new ProductTechnicalSheetModel
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Title = entity.Title,
                Description = entity.Description,
                TechnicalCode = entity.TechnicalCode
            };

            return View("~/Plugins/Product.TechnicalSheet/Views/CreateOrEdit.cshtml", model);
        }

        var newModel = new ProductTechnicalSheetModel
        {
            ProductId = productId
        };

        return View("~/Plugins/Product.TechnicalSheet/Views/CreateOrEdit.cshtml", newModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(ProductTechnicalSheetModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
            return AccessDeniedView();

        if (!ModelState.IsValid)
            return View("~/Plugins/Product.TechnicalSheet/Views/CreateOrEdit.cshtml", model);

        if (model.Id > 0)
        {
            var entity = await _productTechnicalSheetService.GetByIdAsync(model.Id);

            if (entity == null)
                return NotFound();

            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.TechnicalCode = model.TechnicalCode;
            entity.ProductId = model.ProductId;

            await _productTechnicalSheetService.UpdateAsync(entity);
        }
        else
        {
            var entity = new ProductTechnicalSheet
            {
                ProductId = model.ProductId,
                Title = model.Title,
                Description = model.Description,
                TechnicalCode = model.TechnicalCode,
                CreatedOnUtc = DateTime.UtcNow
            };

            await _productTechnicalSheetService.InsertAsync(entity);
        }

        return RedirectToAction("List", new { productId = model.ProductId });
    }
}