namespace Api.Features.ProductCategories.Dtos;

/// <summary>
/// اطلاعات برای ایجاد یک دسته بندی محصول
/// </summary>
public record class CreateProductCategoryDto
{
    /// <summary>
    /// آیدی دسته بندی اصلی محصول
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// توضیحات
    /// </summary>
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// ‍پراپرتی های سئو
    /// </summary>
    public SeoDto? Seo { get; set; } = new SeoDto();
}