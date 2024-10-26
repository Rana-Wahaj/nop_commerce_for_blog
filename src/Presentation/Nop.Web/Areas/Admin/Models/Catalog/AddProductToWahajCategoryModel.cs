using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

/// <summary>
/// Represents a product model to add to the category
/// </summary>
public partial record AddProductToWahajCategoryModel : BaseNopModel
{
    #region Ctor

    public AddProductToWahajCategoryModel()
    {
        SelectedProductIds = new List<int>();
    }
    #endregion

    #region Properties

    public int CategoryId { get; set; }

    public IList<int> SelectedProductIds { get; set; }

    #endregion
}