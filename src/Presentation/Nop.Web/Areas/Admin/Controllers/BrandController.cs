using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Web.Areas.Admin.Controllers;

public partial class BrandController : BaseAdminController
{
    #region Fields

    protected readonly IAclService _aclService;
    protected readonly ICustomerActivityService _customerActivityService;
    protected readonly ICustomerService _customerService;
    protected readonly IDiscountService _discountService;
    protected readonly IExportManager _exportManager;
    protected readonly IImportManager _importManager;
    protected readonly ILocalizationService _localizationService;
    protected readonly ILocalizedEntityService _localizedEntityService;
    protected readonly IBrandModelFactory _brandModelFactory;
    protected readonly IBrandService _brandService;
    protected readonly INotificationService _notificationService;
    protected readonly IPermissionService _permissionService;
    protected readonly IPictureService _pictureService;
    protected readonly IProductService _productService;
    protected readonly IStoreMappingService _storeMappingService;
    protected readonly IStoreService _storeService;
    protected readonly IUrlRecordService _urlRecordService;
    protected readonly IWorkContext _workContext;

    #endregion

    #region Ctor

    public BrandController(IAclService aclService,
        ICustomerActivityService customerActivityService,
        ICustomerService customerService,
        IDiscountService discountService,
        IExportManager exportManager,
        IImportManager importManager,
        ILocalizationService localizationService,
        ILocalizedEntityService localizedEntityService,
      IBrandService brandService,
        IBrandModelFactory brandModelFactory,
        INotificationService notificationService,
        IPermissionService permissionService,
        IPictureService pictureService,
        IProductService productService,
        IStoreMappingService storeMappingService,
        IStoreService storeService,
        IUrlRecordService urlRecordService,
        IWorkContext workContext)
    {
        _aclService = aclService;
        _customerActivityService = customerActivityService;
        _customerService = customerService;
        _discountService = discountService;
        _exportManager = exportManager;
        _importManager = importManager;
        _localizationService = localizationService;
        _localizedEntityService = localizedEntityService;
        _brandModelFactory = brandModelFactory;
        _brandService = brandService;
        _notificationService = notificationService;
        _permissionService = permissionService;
        _pictureService = pictureService;
        _productService = productService;
        _storeMappingService = storeMappingService;
        _storeService = storeService;
        _urlRecordService = urlRecordService;
        _workContext = workContext;
    }

    #endregion

    #region Utilities

    protected virtual async Task UpdateLocalesAsync(Manufacturer manufacturer, ManufacturerModel model)
    {
        foreach (var localized in model.Locales)
        {
            await _localizedEntityService.SaveLocalizedValueAsync(manufacturer,
                x => x.Name,
                localized.Name,
                localized.LanguageId);

            await _localizedEntityService.SaveLocalizedValueAsync(manufacturer,
                x => x.Description,
                localized.Description,
                localized.LanguageId);

            await _localizedEntityService.SaveLocalizedValueAsync(manufacturer,
                x => x.MetaKeywords,
                localized.MetaKeywords,
                localized.LanguageId);

            await _localizedEntityService.SaveLocalizedValueAsync(manufacturer,
                x => x.MetaDescription,
                localized.MetaDescription,
                localized.LanguageId);

            await _localizedEntityService.SaveLocalizedValueAsync(manufacturer,
                x => x.MetaTitle,
                localized.MetaTitle,
                localized.LanguageId);

            //search engine name
            var seName = await _urlRecordService.ValidateSeNameAsync(manufacturer, localized.SeName, localized.Name, false);
            await _urlRecordService.SaveSlugAsync(manufacturer, seName, localized.LanguageId);
        }
    }

    protected virtual async Task UpdatePictureSeoNamesAsync(Manufacturer manufacturer)
    {
        var picture = await _pictureService.GetPictureByIdAsync(manufacturer.PictureId);
        if (picture != null)
            await _pictureService.SetSeoFilenameAsync(picture.Id, await _pictureService.GetPictureSeNameAsync(manufacturer.Name));
    }

    protected virtual async Task SaveManufacturerAclAsync(Brand brand, BrandModel model)
    {
        brand.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
        await _brandService.UpdateBrandAsync(brand);

        var existingAclRecords = await _aclService.GetAclRecordsAsync(brand);
        var allCustomerRoles = await _customerService.GetAllCustomerRolesAsync(true);
        foreach (var customerRole in allCustomerRoles)
        {
            if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
            {
                //new role
                if (!existingAclRecords.Any(acl => acl.CustomerRoleId == customerRole.Id))
                    await _aclService.InsertAclRecordAsync(brand, customerRole.Id);
            }
            else
            {
                //remove role
                var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                if (aclRecordToDelete != null)
                    await _aclService.DeleteAclRecordAsync(aclRecordToDelete);
            }
        }
    }

    protected virtual async Task SaveStoreMappingsAsync(Brand brand, BrandModel model)
    {
        brand.LimitedToStores = model.SelectedStoreIds.Any();
        await _brandService.UpdateBrandAsync(brand);

        var existingStoreMappings = await _storeMappingService.GetStoreMappingsAsync(brand);
        var allStores = await _storeService.GetAllStoresAsync();
        foreach (var store in allStores)
        {
            if (model.SelectedStoreIds.Contains(store.Id))
            {
                //new store
                if (!existingStoreMappings.Any(sm => sm.StoreId == store.Id))
                    await _storeMappingService.InsertStoreMappingAsync(brand, store.Id);
            }
            else
            {
                //remove store
                var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                if (storeMappingToDelete != null)
                    await _storeMappingService.DeleteStoreMappingAsync(storeMappingToDelete);
            }
        }
    }

    #endregion

    #region List

    public virtual IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public virtual async Task<IActionResult> List()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
            return AccessDeniedView();

        //prepare model
        var model = await _brandModelFactory.PrepareBrandSearchModelAsync(new BrandSearchModel());

        return View(model);
    }

    [HttpPost]
    public virtual async Task<IActionResult> List(BrandSearchModel searchModel)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
            return await AccessDeniedDataTablesJson();

        //prepare model
        var model = await _brandModelFactory.PrepareBrandListModelAsync(searchModel);

        return Json(model);
    }

    #endregion

    #region Create / Edit / Delete

    //public virtual async Task<IActionResult> Create()
    //{
    //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
    //        return AccessDeniedView();

    //    //prepare model
    //    var model = await _manufacturerModelFactory.PrepareManufacturerModelAsync(new ManufacturerModel(), null);

    //    return View(model);
    //}

    //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    //public virtual async Task<IActionResult> Create(ManufacturerModel model, bool continueEditing)
    //{
    //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
    //        return AccessDeniedView();

    //    if (ModelState.IsValid)
    //    {
    //        var manufacturer = model.ToEntity<Manufacturer>();
    //        manufacturer.CreatedOnUtc = DateTime.UtcNow;
    //        manufacturer.UpdatedOnUtc = DateTime.UtcNow;
    //        await _manufacturerService.InsertManufacturerAsync(manufacturer);

    //        //search engine name
    //        model.SeName = await _urlRecordService.ValidateSeNameAsync(manufacturer, model.SeName, manufacturer.Name, true);
    //        await _urlRecordService.SaveSlugAsync(manufacturer, model.SeName, 0);

    //        //locales
    //        await UpdateLocalesAsync(manufacturer, model);

    //        //discounts
    //        var allDiscounts = await _discountService.GetAllDiscountsAsync(DiscountType.AssignedToManufacturers, showHidden: true, isActive: null);
    //        foreach (var discount in allDiscounts)
    //        {
    //            if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
    //                //manufacturer.AppliedDiscounts.Add(discount);
    //                await _manufacturerService.InsertDiscountManufacturerMappingAsync(new DiscountManufacturerMapping { EntityId = manufacturer.Id, DiscountId = discount.Id });

    //        }

    //        await _manufacturerService.UpdateManufacturerAsync(manufacturer);

    //        //update picture seo file name
    //        await UpdatePictureSeoNamesAsync(manufacturer);

    //        //ACL (customer roles)
    //        await SaveManufacturerAclAsync(manufacturer, model);

    //        //stores
    //        await SaveStoreMappingsAsync(manufacturer, model);

    //        //activity log
    //        await _customerActivityService.InsertActivityAsync("AddNewManufacturer",
    //            string.Format(await _localizationService.GetResourceAsync("ActivityLog.AddNewManufacturer"), manufacturer.Name), manufacturer);

    //        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Manufacturers.Added"));

    //        if (!continueEditing)
    //            return RedirectToAction("List");

    //        return RedirectToAction("Edit", new { id = manufacturer.Id });
    //    }

    //    //prepare model
    //    model = await _manufacturerModelFactory.PrepareManufacturerModelAsync(model, null, true);

    //    //if we got this far, something failed, redisplay form
    //    return View(model);
    //}

    //public virtual async Task<IActionResult> Edit(int id)
    //{
    //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
    //        return AccessDeniedView();

    //    //try to get a manufacturer with the specified id
    //    var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
    //    if (manufacturer == null || manufacturer.Deleted)
    //        return RedirectToAction("List");

    //    //prepare model
    //    var model = await _manufacturerModelFactory.PrepareManufacturerModelAsync(null, manufacturer);

    //    return View(model);
    //}

    //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    //public virtual async Task<IActionResult> Edit(ManufacturerModel model, bool continueEditing)
    //{
    //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
    //        return AccessDeniedView();

    //    //try to get a manufacturer with the specified id
    //    var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(model.Id);
    //    if (manufacturer == null || manufacturer.Deleted)
    //        return RedirectToAction("List");

    //    if (ModelState.IsValid)
    //    {
    //        var prevPictureId = manufacturer.PictureId;
    //        manufacturer = model.ToEntity(manufacturer);
    //        manufacturer.UpdatedOnUtc = DateTime.UtcNow;
    //        await _manufacturerService.UpdateManufacturerAsync(manufacturer);

    //        //search engine name
    //        model.SeName = await _urlRecordService.ValidateSeNameAsync(manufacturer, model.SeName, manufacturer.Name, true);
    //        await _urlRecordService.SaveSlugAsync(manufacturer, model.SeName, 0);

    //        //locales
    //        await UpdateLocalesAsync(manufacturer, model);

    //        //discounts
    //        var allDiscounts = await _discountService.GetAllDiscountsAsync(DiscountType.AssignedToManufacturers, showHidden: true, isActive: null);
    //        foreach (var discount in allDiscounts)
    //        {
    //            if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
    //            {
    //                //new discount
    //                if (await _manufacturerService.GetDiscountAppliedToManufacturerAsync(manufacturer.Id, discount.Id) is null)
    //                    await _manufacturerService.InsertDiscountManufacturerMappingAsync(new DiscountManufacturerMapping { EntityId = manufacturer.Id, DiscountId = discount.Id });
    //            }
    //            else
    //            {
    //                //remove discount
    //                if (await _manufacturerService.GetDiscountAppliedToManufacturerAsync(manufacturer.Id, discount.Id) is DiscountManufacturerMapping discountManufacturerMapping)
    //                    await _manufacturerService.DeleteDiscountManufacturerMappingAsync(discountManufacturerMapping);
    //            }
    //        }

    //        await _manufacturerService.UpdateManufacturerAsync(manufacturer);

    //        //delete an old picture (if deleted or updated)
    //        if (prevPictureId > 0 && prevPictureId != manufacturer.PictureId)
    //        {
    //            var prevPicture = await _pictureService.GetPictureByIdAsync(prevPictureId);
    //            if (prevPicture != null)
    //                await _pictureService.DeletePictureAsync(prevPicture);
    //        }

    //        //update picture seo file name
    //        await UpdatePictureSeoNamesAsync(manufacturer);

    //        //ACL
    //        await SaveManufacturerAclAsync(manufacturer, model);

    //        //stores
    //        await SaveStoreMappingsAsync(manufacturer, model);

    //        //activity log
    //        await _customerActivityService.InsertActivityAsync("EditManufacturer",
    //            string.Format(await _localizationService.GetResourceAsync("ActivityLog.EditManufacturer"), manufacturer.Name), manufacturer);

    //        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Manufacturers.Updated"));

    //        if (!continueEditing)
    //            return RedirectToAction("List");

    //        return RedirectToAction("Edit", new { id = manufacturer.Id });
    //    }

    //    //prepare model
    //    model = await _manufacturerModelFactory.PrepareManufacturerModelAsync(model, manufacturer, true);

    //    //if we got this far, something failed, redisplay form
    //    return View(model);
    //}

    //[HttpPost]
    //public virtual async Task<IActionResult> Delete(int id)
    //{
    //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
    //        return AccessDeniedView();

    //    //try to get a manufacturer with the specified id
    //    var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
    //    if (manufacturer == null)
    //        return RedirectToAction("List");

    //    await _manufacturerService.DeleteManufacturerAsync(manufacturer);

    //    //activity log
    //    await _customerActivityService.InsertActivityAsync("DeleteManufacturer",
    //        string.Format(await _localizationService.GetResourceAsync("ActivityLog.DeleteManufacturer"), manufacturer.Name), manufacturer);

    //    _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Manufacturers.Deleted"));

    //    return RedirectToAction("List");
    //}

    //[HttpPost]
    //public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
    //{
    //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageBrands))
    //        return await AccessDeniedDataTablesJson();

    //    if (selectedIds == null || !selectedIds.Any())
    //        return NoContent();

    //    var manufacturers = await _manufacturerService.GetManufacturersByIdsAsync(selectedIds.ToArray());
    //    await _manufacturerService.DeleteManufacturersAsync(manufacturers);

    //    var locale = await _localizationService.GetResourceAsync("ActivityLog.DeleteManufacturer");
    //    foreach (var manufacturer in manufacturers)
    //    {
    //        //activity log
    //        await _customerActivityService.InsertActivityAsync("DeleteManufacturer", string.Format(locale, manufacturer.Name), manufacturer);
    //    }

    //    return Json(new { Result = true });
    //}

    #endregion
}