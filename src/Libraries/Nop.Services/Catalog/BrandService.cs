using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Services.Catalog;

public partial class BrandService : IBrandService
{
    #region Fields

    protected readonly CatalogSettings _catalogSettings;
    protected readonly IAclService _aclService;
    protected readonly ICategoryService _categoryService;
    protected readonly ICustomerService _customerService;
    protected readonly IRepository<DiscountManufacturerMapping> _discountManufacturerMappingRepository;
    protected readonly IRepository<Brand> _brandRepository;
    protected readonly IRepository<Product> _productRepository;
    protected readonly IRepository<ProductBrand> _productBrandRepository;
    protected readonly IRepository<ProductCategory> _productCategoryRepository;
    protected readonly IStaticCacheManager _staticCacheManager;
    protected readonly IStoreContext _storeContext;
    protected readonly IStoreMappingService _storeMappingService;
    protected readonly IWorkContext _workContext;

    #endregion

    #region Ctor

    public BrandService(CatalogSettings catalogSettings,
        IAclService aclService,
        ICategoryService categoryService,
        ICustomerService customerService,
        IRepository<DiscountManufacturerMapping> discountManufacturerMappingRepository,
       IRepository<Brand> brands,
        IRepository<Product> productRepository,
        IRepository<ProductBrand> productBrandRepository,
        IRepository<ProductCategory> productCategoryRepository,
        IStaticCacheManager staticCacheManager,
        IStoreContext storeContext,
        IStoreMappingService storeMappingService,
        IWorkContext workContext)
    {
        _catalogSettings = catalogSettings;
        _aclService = aclService;
        _categoryService = categoryService;
        _customerService = customerService;
        _discountManufacturerMappingRepository = discountManufacturerMappingRepository;
        _brandRepository = brands;
        _productRepository = productRepository;
        _productBrandRepository = productBrandRepository;
        _productCategoryRepository = productCategoryRepository;
        _staticCacheManager = staticCacheManager;
        _storeContext = storeContext;
        _storeMappingService = storeMappingService;
        _workContext = workContext;
    }

    #endregion

    #region Methods

    public virtual async Task DeleteBrandAsync(Brand brand)
    {
        await _brandRepository.DeleteAsync(brand);
    }

    public virtual async Task DeleteBrandsAsync(IList<Brand> brands)
    {
        await _brandRepository.DeleteAsync(brands);
    }

    public virtual async Task<IPagedList<Brand>> GetAllBrandsAsync(string brandName = "",
        int storeId = 0,
        int pageIndex = 0,
        int pageSize = int.MaxValue,
        bool showHidden = false,
        bool? overridePublished = null)
    {
        return await _brandRepository.GetAllPagedAsync(async query =>
        {
            if (!showHidden)
                query = query.Where(m => m.Published);
            else if (overridePublished.HasValue)
                query = query.Where(m => m.Published == overridePublished.Value);
            if (!showHidden || storeId > 0)
                query = await _storeMappingService.ApplyStoreMapping(query, storeId);
            if (!showHidden)
            {
                //apply ACL constraints
                var customer = await _workContext.GetCurrentCustomerAsync();
                query = await _aclService.ApplyAcl(query, customer);
            }
            query = query.Where(m => !m.Deleted);
            if (!string.IsNullOrWhiteSpace(brandName))
                query = query.Where(m => m.Name.Contains(brandName));

            return query.OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id);
        }, pageIndex, pageSize);
    }

    public virtual async Task<Brand> GetBrandByIdAsync(int brandId)
    {
        return await _brandRepository.GetByIdAsync(brandId, cache => default);
    }

    public virtual async Task<IList<Brand>> GetBrandsByIdsAsync(int[] brandIds)
    {
        return await _brandRepository.GetByIdsAsync(brandIds, includeDeleted: false);
    }

    public virtual async Task InsertBrandAsync(Brand brand)
    {
        await _brandRepository.InsertAsync(brand);
    }

    public virtual async Task UpdateBrandAsync(Brand brand)
    {
        await _brandRepository.UpdateAsync(brand);
    }


    public virtual async Task<IPagedList<ProductBrand>> GetProductBrandsByBrandIdAsync(int brandId,
       int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
    {
        if (brandId == 0)
            return new PagedList<ProductBrand>(new List<ProductBrand>(), pageIndex, pageSize);

        var query = from pm in _productBrandRepository.Table
                    join p in _productRepository.Table on pm.ProductId equals p.Id
                    where pm.BrandId == brandId && !p.Deleted
                    orderby pm.DisplayOrder, pm.Id
                    select pm;

        if (!showHidden)
        {
            var brandQuery = _productRepository.Table.Where(m => m.Published);

            //apply store mapping constraints
            var store = await _storeContext.GetCurrentStoreAsync();
            brandQuery = await _storeMappingService.ApplyStoreMapping(brandQuery, store.Id);

            //apply ACL constraints
            var customer = await _workContext.GetCurrentCustomerAsync();
            brandQuery = await _aclService.ApplyAcl(brandQuery, customer);

            query = query.Where(pm => brandQuery.Any(m => m.Id == pm.BrandId));
        }

        return await query.ToPagedListAsync(pageIndex, pageSize);
    }


   
    #endregion
}