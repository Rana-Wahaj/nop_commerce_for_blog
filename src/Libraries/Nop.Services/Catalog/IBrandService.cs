using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog;

/// <summary>
/// Brand service
/// </summary>
public partial interface IBrandService
{
    Task DeleteBrandAsync(Brand brand);
    Task DeleteBrandsAsync(IList<Brand> brands);
    Task<IPagedList<Brand>> GetAllBrandsAsync(string brandName = "",
        int storeId = 0,
        int pageIndex = 0,
        int pageSize = int.MaxValue,
        bool showHidden = false,
        bool? overridePublished = null);
    Task<Brand> GetBrandByIdAsync(int brandId);
    Task<IList<Brand>> GetBrandsByIdsAsync(int[] brandIds);
    Task InsertBrandAsync(Brand brand);
    Task UpdateBrandAsync(Brand brand);


    //Task DeleteDiscountBrandMappingAsync(DiscountManufacturerMapping discountManufacturerMapping);
    //Task ClearDiscountBrandServiceMappingAsync(Discount discount);
    //Task<IList<int>> GetAppliedBrandIdsAsync(Discount discount, Customer customer);
    //Task<IList<Brand>> GetBrandsByCategoryIdAsync(int categoryId);
    //Task<IPagedList<Brand>> GetBrandsWithAppliedDiscountAsync(int? discountId = null,
    //    bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue);
    //Task DeleteProductBrandsync(ProductManufacturer productManufacturer);
    
    
    
   

    //Task<IList<ProductManufacturer>> GetProductBrandsByProductIdAsync(int productId, bool showHidden = false);
    //Task<ProductManufacturer> GetProductBrandByIdAsync(int productManufacturerId);
    //Task InsertProductBrandAsync(ProductManufacturer productManufacturer);
    //Task UpdateProductBrandAsync(ProductManufacturer productManufacturer);
    //Task<IDictionary<int, int[]>> GetProductBrandIdsAsync(int[] productIds);
    //Task<string[]> GetNotExistingBrandsAsync(string[] manufacturerIdsNames);
    //ProductManufacturer FindProductBrand(IList<ProductManufacturer> source, int productId, int manufacturerId);
    //Task<DiscountManufacturerMapping> GetDiscountAppliedToBrandAsync(int manufacturerId, int discountId);
    //Task InsertDiscountManufacturerMappingAsync(DiscountManufacturerMapping discountManufacturerMapping);
}