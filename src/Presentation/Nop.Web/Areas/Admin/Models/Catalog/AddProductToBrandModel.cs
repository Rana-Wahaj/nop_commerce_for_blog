using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product model to add to the manufacturer
/// </summary>
public partial record AddProductToBrandModel : BaseNopModel
{
    #region Ctor

    public AddProductToBrandModel()
    {
        SelectedProductIds = new List<int>();
    }
    #endregion

    #region Properties

    public int BrandId { get; set; }

    public IList<int> SelectedProductIds { get; set; }

    #endregion
}