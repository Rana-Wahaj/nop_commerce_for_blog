using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Catalog;

public partial record BrandNavigationModel : BaseNopModel
{
    public BrandNavigationModel()
    {
        Brands = new List<BrandBriefInfoModel>();
    }

    public IList<BrandBriefInfoModel> Brands { get; set; }

    public int TotalBrands { get; set; }
}

public partial record BrandBriefInfoModel : BaseNopEntityModel
{
    public string Name { get; set; }

    public string SeName { get; set; }

    public bool IsActive { get; set; }
}