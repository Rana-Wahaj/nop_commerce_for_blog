﻿namespace Nop.Core.Domain.Catalog;

/// <summary>
/// Represents a product manufacturer mapping
/// </summary>
public partial class ProductBrand : BaseEntity
{
    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer identifier
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is featured
    /// </summary>
    public bool IsFeaturedProduct { get; set; }

    /// <summary>
    /// Gets or sets the display order
    /// </summary>
    public int DisplayOrder { get; set; }
}