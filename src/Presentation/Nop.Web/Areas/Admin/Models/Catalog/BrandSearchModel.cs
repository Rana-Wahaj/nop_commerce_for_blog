﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a manufacturer search model
/// </summary>
public partial record BrandSearchModel : BaseSearchModel
{
    #region Ctor

    public BrandSearchModel()
    {
        AvailableStores = new List<SelectListItem>();
        AvailablePublishedOptions = new List<SelectListItem>();
    }

    #endregion

    #region Properties

    [NopResourceDisplayName("Admin.Catalog.Brands.List.SearchBrandName")]
    public string SearchBrandName { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.List.SearchStore")]
    public int SearchStoreId { get; set; }

    public IList<SelectListItem> AvailableStores { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Brands.List.SearchPublished")]
    public int SearchPublishedId { get; set; }

    public IList<SelectListItem> AvailablePublishedOptions { get; set; }

    public bool HideStoresList { get; set; }

    #endregion
}