using Nop.Core.Domain.Catalog;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Web.Areas.Admin.Factories;

public partial interface IWahajCategoryModelFactory
{
    /// <summary>
    /// Prepare category search model
    /// </summary>
    /// <param name="searchModel">Category search model</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category search model
    /// </returns>
    Task<WahajCategorySearchModel> PrepareCategorySearchModelAsync(WahajCategorySearchModel searchModel);

    /// <summary>
    /// Prepare paged category list model
    /// </summary>
    /// <param name="searchModel">Category search model</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category list model
    /// </returns>
    Task<WahajCategoryListModel> PrepareCategoryListModelAsync(WahajCategorySearchModel searchModel);

    /// <summary>
    /// Prepare category model
    /// </summary>
    /// <param name="model">Category model</param>
    /// <param name="category">Category</param>
    /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category model
    /// </returns>
    Task<WahajCategoryModel> PrepareCategoryModelAsync(WahajCategoryModel model, CategoryWahaj category, bool excludeProperties = false);

    /// <summary>
    /// Prepare paged category product list model
    /// </summary>
    /// <param name="searchModel">Category product search model</param>
    /// <param name="category">Category</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category product list model
    /// </returns>
    Task<WahajCategoryProductListModel> PrepareCategoryProductListModelAsync(WahajCategoryProductSearchModel searchModel, CategoryWahaj category);

    /// <summary>
    /// Prepare product search model to add to the category
    /// </summary>
    /// <param name="searchModel">Product search model to add to the category</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the product search model to add to the category
    /// </returns>
    Task<AddProductToWahajCategorySearchModel> PrepareAddProductToCategorySearchModelAsync(AddProductToWahajCategorySearchModel searchModel);

    /// <summary>
    /// Prepare paged product list model to add to the category
    /// </summary>
    /// <param name="searchModel">Product search model to add to the category</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the product list model to add to the category
    /// </returns>
    Task<AddProductToWahajCategoryListModel> PrepareAddProductToCategoryListModelAsync(AddProductToWahajCategorySearchModel searchModel);
}
