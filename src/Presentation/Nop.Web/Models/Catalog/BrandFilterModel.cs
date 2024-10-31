using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Catalog;

/// <summary>
/// Represents a manufacturer filter model
/// </summary>
public partial record BrandFilterModel : BaseNopModel
{
    #region Properties

    /// <summary>
    /// Gets or sets a value indicating whether filtering is enabled
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the filtrable manufacturers
    /// </summary>
    public IList<SelectListItem> Brands { get; set; }

    #endregion

    #region Ctor

    public BrandFilterModel()
    {
        Brands = new List<SelectListItem>();
    }

    #endregion
}