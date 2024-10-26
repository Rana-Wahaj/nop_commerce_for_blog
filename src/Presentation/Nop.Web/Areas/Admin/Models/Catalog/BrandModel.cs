using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a manufacturer model
/// </summary>
public partial record BrandModel : BaseNopEntityModel, IAclSupportedModel, IDiscountSupportedModel,
    ILocalizedModel<BrandLocalizedModel>, IStoreMappingSupportedModel
{
    #region Ctor

    public BrandModel()
    {
        if (PageSize < 1)
        {
            PageSize = 5;
        }
        Locales = new List<BrandLocalizedModel>();
        AvailableBrandTemplates = new List<SelectListItem>();

        AvailableDiscounts = new List<SelectListItem>();
        SelectedDiscountIds = new List<int>();

        SelectedCustomerRoleIds = new List<int>();
        AvailableCustomerRoles = new List<SelectListItem>();

        SelectedStoreIds = new List<int>();
        AvailableStores = new List<SelectListItem>();

        ManufacturerProductSearchModel = new ManufacturerProductSearchModel();
    }

    #endregion

    #region Properties

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Name")]
    public string Name { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Description")]
    public string Description { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.BrandsTemplate")]
    public int ManufacturerTemplateId { get; set; }

    public IList<SelectListItem> AvailableBrandTemplates { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.MetaKeywords")]
    public string MetaKeywords { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.MetaDescription")]
    public string MetaDescription { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.MetaTitle")]
    public string MetaTitle { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.SeName")]
    public string SeName { get; set; }

    [UIHint("Picture")]
    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Picture")]
    public int PictureId { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.PageSize")]
    public int PageSize { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.AllowCustomersToSelectPageSize")]
    public bool AllowCustomersToSelectPageSize { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.PageSizeOptions")]
    public string PageSizeOptions { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.PriceRangeFiltering")]
    public bool PriceRangeFiltering { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.PriceFrom")]
    public decimal PriceFrom { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.PriceTo")]
    public decimal PriceTo { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.ManuallyPriceRange")]
    public bool ManuallyPriceRange { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Published")]
    public bool Published { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Deleted")]
    public bool Deleted { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.DisplayOrder")]
    public int DisplayOrder { get; set; }

    public IList<BrandLocalizedModel> Locales { get; set; }

    //ACL (customer roles)
    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.AclCustomerRoles")]
    public IList<int> SelectedCustomerRoleIds { get; set; }
    public IList<SelectListItem> AvailableCustomerRoles { get; set; }

    //store mapping
    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.LimitedToStores")]
    public IList<int> SelectedStoreIds { get; set; }
    public IList<SelectListItem> AvailableStores { get; set; }

    //discounts
    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Discounts")]
    public IList<int> SelectedDiscountIds { get; set; }
    public IList<SelectListItem> AvailableDiscounts { get; set; }

    public ManufacturerProductSearchModel ManufacturerProductSearchModel { get; set; }

    public string PrimaryStoreCurrencyCode { get; set; }

    #endregion
}

public partial record BrandLocalizedModel : ILocalizedLocaleModel
{
    public int LanguageId { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Name")]
    public string Name { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.Description")]
    public string Description { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.MetaKeywords")]
    public string MetaKeywords { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.MetaDescription")]
    public string MetaDescription { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.MetaTitle")]
    public string MetaTitle { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.Fields.SeName")]
    public string SeName { get; set; }
}