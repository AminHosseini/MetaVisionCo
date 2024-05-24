namespace Api.Features.Pictures.Dtos;

/// <summary>
/// اطلاعات برای ویرایش یک دسته بندی محصول
/// </summary>
public record class UpdatePictureDto
{
    /// <summary>
    /// آلت عکس برای سئو
    /// </summary>
    public string? PictureAlt { get; set; }

    /// <summary>
    /// عنوان عکس برای سئو
    /// </summary>
    public string? PictureTitle { get; set; }

    /// <summary>
    /// ترتیب نمایش
    /// </summary>
    public required int DisplayOrder { get; set; }

    /// <summary>
    /// دسته بندی محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; set; }
}
