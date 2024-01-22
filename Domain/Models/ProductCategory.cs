﻿namespace Domain.Models;

/// <summary>
/// دسته بندی محصول
/// </summary>
public class ProductCategory : BaseEntity, IHaveSoftDelete
{
    /// <summary>
    /// آیدی دسته بندی اصلی محصول
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// ‍پراپرتی های سئو
    /// </summary>
    public Seo Seo { get; set; }

    /// <summary>
    /// ‍پراپرتی های عکس
    /// </summary>
    public required Picture Picture { get; set; }

    [SetsRequiredMembers]
    public ProductCategory()
    {
        Name = string.Empty;
        Description = string.Empty;
        Seo = new Seo();
        Picture = new Picture();
    }

    [SetsRequiredMembers]
    public ProductCategory(long id)
    {
        Id = id;
        Name = string.Empty;
        Description = string.Empty;
        Seo = new Seo();
        Picture = new Picture();
    }
}
