using Nop.Core.Domain.Catalog;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Web.Areas.Admin.Factories;

/// <summary>
/// Represents the manufacturer model factory
/// </summary>
public partial interface IBrandModelFactory
{

    Task<BrandSearchModel> PrepareBrandSearchModelAsync(BrandSearchModel searchModel);

    Task<BrandListModel> PrepareBrandListModelAsync(BrandSearchModel searchModel);

  
    Task<BrandModel> PrepareBrandModelAsync(BrandModel model, Brand brand, bool excludeProperties = false);

    Task<BrandProductListModel> PrepareBrandProductListModelAsync(BrandProductSearchModel searchModel, Brand brand);

    Task<AddProductToBrandSearchModel> PrepareAddProductToBrandSearchModelAsync(AddProductToBrandSearchModel searchModel);


    Task<AddProductToManufacturerListModel> PrepareAddProductToManufacturerListModelAsync(AddProductToManufacturerSearchModel searchModel);
}