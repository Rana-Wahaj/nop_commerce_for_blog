﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog;

public partial record WahajCategorySearchModel : BaseSearchModel
{
    #region Ctor

    public WahajCategorySearchModel()
    {
        AvailableStores = new List<SelectListItem>();
        AvailablePublishedOptions = new List<SelectListItem>();
    }

    #endregion

    #region Properties

    [NopResourceDisplayName("Admin.Catalog.Categories.List.SearchCategoryName")]
    public string SearchCategoryName { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Categories.List.SearchPublished")]
    public int SearchPublishedId { get; set; }

    public IList<SelectListItem> AvailablePublishedOptions { get; set; }

    [NopResourceDisplayName("Admin.Catalog.Categories.List.SearchStore")]
    public int SearchStoreId { get; set; }

    public IList<SelectListItem> AvailableStores { get; set; }

    public bool HideStoresList { get; set; }

    #endregion
}
