using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Web.Areas.Admin.Factories;

public partial class BrandModelFactory : IBrandModelFactory
{
    #region Fields

    protected readonly CatalogSettings _catalogSettings;
    protected readonly CurrencySettings _currencySettings;
    protected readonly ICurrencyService _currencyService;
    protected readonly IAclSupportedModelFactory _aclSupportedModelFactory;
    protected readonly IBaseAdminModelFactory _baseAdminModelFactory;
    protected readonly IBrandService _brandService;
    protected readonly IDiscountService _discountService;
    protected readonly IDiscountSupportedModelFactory _discountSupportedModelFactory;
    protected readonly ILocalizationService _localizationService;
    protected readonly ILocalizedModelFactory _localizedModelFactory;
    protected readonly IProductService _productService;
    protected readonly IStoreMappingSupportedModelFactory _storeMappingSupportedModelFactory;
    protected readonly IUrlRecordService _urlRecordService;

    #endregion

    #region Ctor

    public BrandModelFactory(CatalogSettings catalogSettings,
        CurrencySettings currencySettings,
        ICurrencyService currencyService,
        IAclSupportedModelFactory aclSupportedModelFactory,
        IBaseAdminModelFactory baseAdminModelFactory,
        IBrandService brandService,
        IDiscountService discountService,
        IDiscountSupportedModelFactory discountSupportedModelFactory,
        ILocalizationService localizationService,
        ILocalizedModelFactory localizedModelFactory,
        IProductService productService,
        IStoreMappingSupportedModelFactory storeMappingSupportedModelFactory,
        IUrlRecordService urlRecordService)
    {
        _catalogSettings = catalogSettings;
        _currencySettings = currencySettings;
        _currencyService = currencyService;
        _aclSupportedModelFactory = aclSupportedModelFactory;
        _baseAdminModelFactory = baseAdminModelFactory;
        _brandService = brandService;
        _discountService = discountService;
        _discountSupportedModelFactory = discountSupportedModelFactory;
        _localizationService = localizationService;
        _localizedModelFactory = localizedModelFactory;
        _productService = productService;
        _storeMappingSupportedModelFactory = storeMappingSupportedModelFactory;
        _urlRecordService = urlRecordService;
    }

    #endregion

    #region Utilities

 
    protected virtual BrandProductSearchModel PrepareBrandProductSearchModel(BrandProductSearchModel searchModel,
        Brand brand)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        ArgumentNullException.ThrowIfNull(brand);

        searchModel.BrandId = brand.Id;

        searchModel.SetGridPageSize();

        return searchModel;
    }

    #endregion

    #region Methods

    
    public virtual async Task<BrandSearchModel> PrepareBrandSearchModelAsync(BrandSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

       
        await _baseAdminModelFactory.PrepareStoresAsync(searchModel.AvailableStores);

        searchModel.HideStoresList = _catalogSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

        
        searchModel.AvailablePublishedOptions.Add(new SelectListItem
        {
            Value = "0",
            Text = await _localizationService.GetResourceAsync("Admin.Catalog.Brands.List.SearchPublished.All")
        });
        searchModel.AvailablePublishedOptions.Add(new SelectListItem
        {
            Value = "1",
            Text = await _localizationService.GetResourceAsync("Admin.Catalog.Brands.List.SearchPublished.PublishedOnly")
        });
        searchModel.AvailablePublishedOptions.Add(new SelectListItem
        {
            Value = "2",
            Text = await _localizationService.GetResourceAsync("Admin.Catalog.Brands.List.SearchPublished.UnpublishedOnly")
        });

      
        searchModel.SetGridPageSize();

        return searchModel;
    }

    public virtual async Task<BrandListModel> PrepareBrandListModelAsync(BrandSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

      
        var brand = await _brandService.GetAllBrandsAsync(showHidden: true,
            brandName: searchModel.SearchBrandName,
            storeId: searchModel.SearchStoreId,
            pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,
            overridePublished: searchModel.SearchPublishedId == 0 ? null : (bool?)(searchModel.SearchPublishedId == 1));

    
        var model = await new BrandListModel().PrepareToGridAsync(searchModel, brand, () =>
        {
      
            return brand.SelectAwait(async brand =>
            {
                var brandModel = brand.ToModel<BrandModel>();

                brandModel.SeName = await _urlRecordService.GetSeNameAsync(brand, 0, true, false);

                return brandModel;
            });
        });

        return model;
    }

   
     public virtual async Task<BrandModel> PrepareBrandModelAsync(BrandModel model, Brand brand , bool excludeProperties = false)
     {
         Func<BrandLocalizedModel, int, Task> localizedModelConfiguration = null;

         if (brand != null)
         {
             
             if (model == null)
             {
                 model = brand.ToModel<BrandModel>();
                 model.SeName = await _urlRecordService.GetSeNameAsync(brand, 0, true, false);
             }

            
             PrepareBrandProductSearchModel(model.BrandProductSearchModel, brand);

          
             localizedModelConfiguration = async (locale, languageId) =>
             {
                 locale.Name = await _localizationService.GetLocalizedAsync(brand, entity => entity.Name, languageId, false, false);
                 locale.Description = await _localizationService.GetLocalizedAsync(brand, entity => entity.Description, languageId, false, false);
                 locale.MetaKeywords = await _localizationService.GetLocalizedAsync(brand, entity => entity.MetaKeywords, languageId, false, false);
                 locale.MetaDescription = await _localizationService.GetLocalizedAsync(brand, entity => entity.MetaDescription, languageId, false, false);
                 locale.MetaTitle = await _localizationService.GetLocalizedAsync(brand, entity => entity.MetaTitle, languageId, false, false);
                 locale.SeName = await _urlRecordService.GetSeNameAsync(brand, languageId, false, false);
             };
         }

         if (brand == null)
         {
             model.PageSize = _catalogSettings.DefaultManufacturerPageSize;
             model.PageSizeOptions = _catalogSettings.DefaultManufacturerPageSizeOptions;
             model.Published = true;
             model.AllowCustomersToSelectPageSize = true;
             model.PriceRangeFiltering = true;
             model.ManuallyPriceRange = true;
             model.PriceFrom = NopCatalogDefaults.DefaultPriceRangeFrom;
             model.PriceTo = NopCatalogDefaults.DefaultPriceRangeTo;
         }

         model.PrimaryStoreCurrencyCode = (await _currencyService.GetCurrencyByIdAsync(_currencySettings.PrimaryStoreCurrencyId)).CurrencyCode;

         if (!excludeProperties)
             model.Locales = await _localizedModelFactory.PrepareLocalizedModelsAsync(localizedModelConfiguration);

        
         await _baseAdminModelFactory.PrepareBrandTemplatesAsync(model.AvailableBrandTemplates, false);

         var availableDiscounts = await _discountService.GetAllDiscountsAsync( DiscountType.AssignedToBrand, showHidden: true, isActive: null);
         await _discountSupportedModelFactory.PrepareModelDiscountsAsync(model, brand, availableDiscounts, excludeProperties);

        await _aclSupportedModelFactory.PrepareModelCustomerRolesAsync(model, brand, excludeProperties);

        await _storeMappingSupportedModelFactory.PrepareModelStoresAsync(model, brand, excludeProperties);

        return model;
    }

    
     public virtual async Task<BrandProductListModel> PrepareBrandProductListModelAsync(BrandProductSearchModel searchModel,
         Brand brand)
     {
         ArgumentNullException.ThrowIfNull(searchModel);

         ArgumentNullException.ThrowIfNull(brand);

         var productBrand = await _brandService.GetProductBrandsByBrandIdAsync(showHidden: true,
            brandId: brand.Id,
             pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

       
         var model = await new BrandProductListModel().PrepareToGridAsync(searchModel, productBrand, () =>
         {
             return productBrand.SelectAwait(async productBrand =>
             {
                  
                 var brandProductModel = productBrand.ToModel<BrandProductModel>();

                 
               brandProductModel.ProductName = (await _productService.GetProductByIdAsync(productBrand.ProductId))?.Name;

                 return brandProductModel;
             });
         });

         return model;
     }

    
     public virtual async Task<AddProductToBrandSearchModel> PrepareAddProductToBrandSearchModelAsync(AddProductToBrandSearchModel searchModel)
     {
         ArgumentNullException.ThrowIfNull(searchModel);


         await _baseAdminModelFactory.PrepareCategoriesAsync(searchModel.AvailableCategories);

         await _baseAdminModelFactory.PrepareManufacturersAsync(searchModel.AvailableBrand);

        
         await _baseAdminModelFactory.PrepareStoresAsync(searchModel.AvailableStores);

         await _baseAdminModelFactory.PrepareVendorsAsync(searchModel.AvailableVendors);

       
         await _baseAdminModelFactory.PrepareProductTypesAsync(searchModel.AvailableProductTypes);

     
         searchModel.SetPopupGridPageSize();

         return searchModel;
     }

    
     public virtual async Task<AddProductToBrandListModel> PrepareAddProductToBrandListModelAsync(AddProductToBrandSearchModel searchModel)
     {
         ArgumentNullException.ThrowIfNull(searchModel);

        
         var products = await _productService.SearchProductsAsync(showHidden: true,
             categoryIds: new List<int> { searchModel.SearchCategoryId },
             manufacturerIds: new List<int> { searchModel.SearchBrandId },
             storeId: searchModel.SearchStoreId,
             vendorId: searchModel.SearchVendorId,
             productType: searchModel.SearchProductTypeId > 0 ? (ProductType?)searchModel.SearchProductTypeId : null,
             keywords: searchModel.SearchProductName,
             pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);


         var model = await new AddProductToBrandListModel().PrepareToGridAsync(searchModel, products, () =>
         {
             return products.SelectAwait(async product =>
             {
                 var productModel = product.ToModel<ProductModel>();

                 productModel.SeName = await _urlRecordService.GetSeNameAsync(product, 0, true, false);

                 return productModel;
             });
         });

         return model;
     }

 



    #endregion
}