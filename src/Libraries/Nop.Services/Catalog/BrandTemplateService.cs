using Nop.Core.Domain.Catalog;
using Nop.Data;

namespace Nop.Services.Catalog;

/// <summary>
/// Manufacturer template service
/// </summary>
public partial class BrandTemplateService : IBrandTemplateService
{
    #region Fields

    protected readonly IRepository<BrandTemplate> _brandTemplateRepository;

    #endregion

    #region Ctor

    public BrandTemplateService(IRepository<BrandTemplate> brandTemplateRepository)
    {
        _brandTemplateRepository = brandTemplateRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Delete manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplate">Manufacturer template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task DeleteBrandTemplateAsync(BrandTemplate brandTemplate)
    {
        await _brandTemplateRepository.DeleteAsync(brandTemplate);
    }

    /// <summary>
    /// Gets all manufacturer templates
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the manufacturer templates
    /// </returns>
    public virtual async Task<IList<BrandTemplate>> GetAllBrandTemplatesAsync()
    {
        var templates = await _brandTemplateRepository.GetAllAsync(query =>
        {
            return from pt in query
                orderby pt.DisplayOrder, pt.Id
                select pt;
        }, cache => default);

        return templates;
    }

    /// <summary>
    /// Gets a manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplateId">Manufacturer template identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the manufacturer template
    /// </returns>
    public virtual async Task<BrandTemplate> GetBrandTemplateByIdAsync(int brandTemplateId)
    {
        return await _brandTemplateRepository.GetByIdAsync(brandTemplateId, cache => default);
    }

    /// <summary>
    /// Inserts manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplate">Manufacturer template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task InsertBrandTemplateAsync(BrandTemplate brandTemplate)
    {
        await _brandTemplateRepository.InsertAsync(brandTemplate);
    }

    /// <summary>
    /// Updates the manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplate">Manufacturer template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public virtual async Task UpdateBrandTemplateAsync(BrandTemplate brandTemplate)
    {
        await _brandTemplateRepository.UpdateAsync(brandTemplate);
    }

    #endregion
}