namespace Api.Features.Commons.Dtos;

/// <summary>
/// ‍برای گرفتن پراپرتی های سئو
/// </summary>
public record class SeoDto
{
    /// <summary>
    /// اسلاگ دسته بندی محصول برای سئو
    /// </summary>
    public string? Slug { get; set; } = string.Empty;

    /// <summary>
    /// کلمات کلیدی برای سئو
    /// </summary>
    public IEnumerable<string>? Keywords { get; set; } = new List<string>();

    /// <summary>
    /// توضیحات متا برای سئو
    /// </summary>
    public string? MetaDescription { get; set; } = string.Empty;
}