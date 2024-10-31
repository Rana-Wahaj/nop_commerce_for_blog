using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog;

/// <summary>
/// Manufacturer template service interface
/// </summary>
public partial interface IBrandTemplateService
{
    /// <summary>
    /// Delete manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplate">Manufacturer template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteBrandTemplateAsync(BrandTemplate brandTemplate);

    /// <summary>
    /// Gets all manufacturer templates
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the manufacturer templates
    /// </returns>
    Task<IList<BrandTemplate>> GetAllBrandTemplatesAsync();

    /// <summary>
    /// Gets a manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplateId">Manufacturer template identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the manufacturer template
    /// </returns>
    Task<BrandTemplate> GetBrandTemplateByIdAsync(int brandTemplateId);

    /// <summary>
    /// Inserts manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplate">Manufacturer template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertBrandTemplateAsync(BrandTemplate brandTemplate);

    /// <summary>
    /// Updates the manufacturer template
    /// </summary>
    /// <param name="manufacturerTemplate">Manufacturer template</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task UpdateBrandTemplateAsync(BrandTemplate brandTemplate);
}