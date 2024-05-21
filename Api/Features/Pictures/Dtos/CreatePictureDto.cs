namespace Api.Features.Pictures.Dtos;

/// <summary>
/// اطلاعات برای ایجاد یک عکس
/// </summary>
public record class CreatePictureDto
{
    /// <summary>
    /// آیدی صاحب عکس
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// نام عکس
    /// </summary>
    public IFormFile? PictureName { get; set; }

    /// <summary>
    /// دگرساز عکس
    /// </summary>
    public string? PictureAlt { get; set; } = string.Empty;

    /// <summary>
    /// عنوان عکس
    /// </summary>
    public string? PictureTitle { get; set; } = string.Empty;

    /// <summary>
    /// ترتیب نمایش
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// نوع عکس
    /// </summary>
    public PictureType PictureType { get; set; }

    /// <summary>
    /// اسلاگ صاحب عکس
    /// </summary>
    public string ParentSlug { get; set; } = string.Empty;
}
