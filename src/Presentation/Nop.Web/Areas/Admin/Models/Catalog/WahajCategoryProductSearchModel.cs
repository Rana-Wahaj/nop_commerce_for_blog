using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog;

public partial record WahajCategoryProductSearchModel : BaseSearchModel
{
    #region Properties

    public int CategoryId { get; set; }

    #endregion
}
