﻿using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;


namespace Nop.Services.Catalog;
public partial interface IWahajCategoryService
{
    /// <summary>
    /// Clean up category references for a specified discount
    /// </summary>
    /// <param name="discount">Discount</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task ClearDiscountCategoryMappingAsync(Discount discount);

    /// <summary>
    /// Delete category
    /// </summary>
    /// <param name="category">Category</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteCategoryAsync(CategoryWahaj category);

    /// <summary>
    /// Gets all categories
    /// </summary>
    /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
    /// <param name="showHidden">A value indicating whether to show hidden records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the categories
    /// </returns>
    Task<IList<CategoryWahaj>> GetAllCategoriesAsync(int storeId = 0, bool showHidden = false);

    /// <summary>
    /// Gets all categories
    /// </summary>
    /// <param name="categoryName">Category name</param>
    /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="showHidden">A value indicating whether to show hidden records</param>
    /// <param name="overridePublished">
    /// null - process "Published" property according to "showHidden" parameter
    /// true - load only "Published" products
    /// false - load only "Unpublished" products
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the categories
    /// </returns>
    Task<IPagedList<CategoryWahaj>> GetAllCategoriesAsync(string categoryName, int storeId = 0,
        int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? overridePublished = null);

    /// <summary>
    /// Gets all categories filtered by parent category identifier
    /// </summary>
    /// <param name="parentCategoryId">Parent category identifier</param>
    /// <param name="showHidden">A value indicating whether to show hidden records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the categories
    /// </returns>
    Task<IList<CategoryWahaj>> GetAllCategoriesByParentCategoryIdAsync(int parentCategoryId, bool showHidden = false);

    /// <summary>
    /// Gets all categories displayed on the home page
    /// </summary>
    /// <param name="showHidden">A value indicating whether to show hidden records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the categories
    /// </returns>
    Task<IList<CategoryWahaj>> GetAllCategoriesDisplayedOnHomepageAsync(bool showHidden = false);

    /// <summary>
    /// Get category identifiers to which a discount is applied
    /// </summary>
    /// <param name="discount">Discount</param>
    /// <param name="customer">Customer</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category identifiers
    /// </returns>
    Task<IList<int>> GetAppliedCategoryIdsAsync(Discount discount, Customer customer);

    /// <summary>
    /// Gets child category identifiers
    /// </summary>
    /// <param name="parentCategoryId">Parent category identifier</param>
    /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
    /// <param name="showHidden">A value indicating whether to show hidden records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category identifiers
    /// </returns>
    Task<IList<int>> GetChildCategoryIdsAsync(int parentCategoryId, int storeId = 0, bool showHidden = false);

    /// <summary>
    /// Gets a category
    /// </summary>
    /// <param name="categoryId">Category identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category
    /// </returns>
    Task<CategoryWahaj> GetCategoryByIdAsync(int categoryId);

    /// <summary>
    /// Get categories for which a discount is applied
    /// </summary>
    /// <param name="discountId">Discount identifier; pass null to load all records</param>
    /// <param name="showHidden">A value indicating whether to load deleted categories</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the list of categories
    /// </returns>
    Task<IPagedList<CategoryWahaj>> GetCategoriesByAppliedDiscountAsync(int? discountId = null,
        bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue);

    /// <summary>
    /// Inserts category
    /// </summary>
    /// <param name="category">Category</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertCategoryAsync(CategoryWahaj category);

    /// <summary>
    /// Updates the category
    /// </summary>
    /// <param name="category">Category</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task UpdateCategoryAsync(CategoryWahaj category);

    /// <summary>
    /// Delete a list of categories
    /// </summary>
    /// <param name="categories">Categories</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteCategoriesAsync(IList<CategoryWahaj> categories);

    /// <summary>
    /// Deletes a product category mapping
    /// </summary>
    /// <param name="productCategory">Product category</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteProductCategoryAsync(WahajProductCategory productCategory);

    /// <summary>
    /// Get a discount-category mapping record
    /// </summary>
    /// <param name="categoryId">Category identifier</param>
    /// <param name="discountId">Discount identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the result
    /// </returns>
    Task<DiscountCategoryMapping> GetDiscountAppliedToCategoryAsync(int categoryId, int discountId);

    /// <summary>
    /// Inserts a discount-category mapping record
    /// </summary>
    /// <param name="discountCategoryMapping">Discount-category mapping</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertDiscountCategoryMappingAsync(DiscountCategoryMapping discountCategoryMapping);

    /// <summary>
    /// Deletes a discount-category mapping record
    /// </summary>
    /// <param name="discountCategoryMapping">Discount-category mapping</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteDiscountCategoryMappingAsync(DiscountCategoryMapping discountCategoryMapping);

    /// <summary>
    /// Gets product category mapping collection
    /// </summary>
    /// <param name="categoryId">Category identifier</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="showHidden">A value indicating whether to show hidden records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the product a category mapping collection
    /// </returns>
    Task<IPagedList<WahajProductCategory>> GetProductCategoriesByCategoryIdAsync(int categoryId,
        int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

    /// <summary>
    /// Gets a product category mapping collection
    /// </summary>
    /// <param name="productId">Product identifier</param>
    /// <param name="showHidden">A value indicating whether to show hidden records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the product category mapping collection
    /// </returns>
    Task<IList<WahajProductCategory>> GetProductCategoriesByProductIdAsync(int productId, bool showHidden = false);

    /// <summary>
    /// Gets a product category mapping 
    /// </summary>
    /// <param name="productCategoryId">Product category mapping identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the product category mapping
    /// </returns>
    Task<WahajProductCategory> GetProductCategoryByIdAsync(int productCategoryId);

    /// <summary>
    /// Inserts a product category mapping
    /// </summary>
    /// <param name="productCategory">>Product category mapping</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertProductCategoryAsync(WahajProductCategory productCategory);

    /// <summary>
    /// Updates the product category mapping 
    /// </summary>
    /// <param name="productCategory">>Product category mapping</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task UpdateProductCategoryAsync(WahajProductCategory productCategory);

    /// <summary>
    /// Returns a list of names of not existing categories
    /// </summary>
    /// <param name="categoryIdsNames">The names and/or IDs of the categories to check</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the list of names and/or IDs not existing categories
    /// </returns>
    Task<string[]> GetNotExistingCategoriesAsync(string[] categoryIdsNames);

    /// <summary>
    /// Get category IDs for products
    /// </summary>
    /// <param name="productIds">Products IDs</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category IDs for products
    /// </returns>
    Task<IDictionary<int, int[]>> GetProductCategoryIdsAsync(int[] productIds);

    /// <summary>
    /// Gets categories by identifier
    /// </summary>
    /// <param name="categoryIds">Category identifiers</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the categories
    /// </returns>
    Task<IList<CategoryWahaj>> GetCategoriesByIdsAsync(int[] categoryIds);

    /// <summary>
    /// Returns a ProductCategory that has the specified values
    /// </summary>
    /// <param name="source">Source</param>
    /// <param name="productId">Product identifier</param>
    /// <param name="categoryId">Category identifier</param>
    /// <returns>A ProductCategory that has the specified values; otherwise null</returns>
    WahajProductCategory FindProductCategory(IList<WahajProductCategory> source, int productId, int categoryId);

    /// <summary>
    /// Get formatted category breadcrumb 
    /// Note: ACL and store mapping is ignored
    /// </summary>
    /// <param name="category">Category</param>
    /// <param name="allCategories">All categories</param>
    /// <param name="separator">Separator</param>
    /// <param name="languageId">Language identifier for localization</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the formatted breadcrumb
    /// </returns>
    Task<string> GetFormattedBreadCrumbAsync(CategoryWahaj category, IList<CategoryWahaj> allCategories = null,
        string separator = ">>", int languageId = 0);

    /// <summary>
    /// Get category breadcrumb 
    /// </summary>
    /// <param name="category">Category</param>
    /// <param name="allCategories">All categories</param>
    /// <param name="showHidden">A value indicating whether to load hidden records</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the category breadcrumb 
    /// </returns>
    Task<IList<CategoryWahaj>> GetCategoryBreadCrumbAsync(CategoryWahaj category, IList<CategoryWahaj> allCategories = null, bool showHidden = false);
}
