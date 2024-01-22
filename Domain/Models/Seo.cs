namespace Domain.Models;

/// <summary>
/// ‍پراپرتی های سئو
/// </summary>
public class Seo
{
    /// <summary>
    /// اسلاگ برای سئو
    /// </summary>
    public required string Slug { get; set; }

    /// <summary>
    /// کلمات کلیدی برای سئو
    /// </summary>
    public required string Keywords { get; set; }

    /// <summary>
    /// توضیحات متا برای سئو
    /// </summary>
    public required string MetaDescription { get; set; }

    [SetsRequiredMembers]
    public Seo()
    {
        Slug = string.Empty;
        Keywords = string.Empty;
        MetaDescription = string.Empty;
    }
}
