using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a manufacturer product search model
/// </summary>
public partial record BrandProductSearchModel : BaseSearchModel
{
    #region Properties

    public int BrandId { get; set; }

    #endregion
}