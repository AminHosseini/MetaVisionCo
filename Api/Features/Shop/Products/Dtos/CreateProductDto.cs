namespace Api.Features.Shop.Products.Dtos;

/// <summary>
/// اطلاعات برای ایجاد یک محصول
/// </summary>
public record class CreateProductDto
{
    /// <summary>
    /// آیدی دسته بندی محصول
    /// </summary>
    public long? ProductCategoryId { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// کد محصولی
    /// </summary>
    //public string? Code { get; set; } = string.Empty;

    /// <summary>
    /// توضیحات کوتاه
    /// </summary>
    public string? ShortDescription { get; set; } = string.Empty;

    /// <summary>
    /// توضیحات
    /// </summary>
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// ‍پراپرتی های سئو
    /// </summary>
    public SeoDto? Seo { get; set; } = new SeoDto();
}
