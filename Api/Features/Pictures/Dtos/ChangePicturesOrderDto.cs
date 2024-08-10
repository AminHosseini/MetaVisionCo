namespace Api.Features.Pictures.Dtos;

/// <summary>
/// اطلاعات برای ویرایش ترتیب نمایش عکس ها
/// </summary>
public record class ChangePicturesOrderDto
{
    /// <summary>
    /// آیدی عکس
    /// </summary>
    public required long PictureId { get; set; }

    /// <summary>
    /// ترتیب نمایش
    /// </summary>
    public required int DisplayOrder { get; set; }

    /// <summary>
    /// نوع صاحب عکس
    /// </summary>
    public required PictureType PictureType { get; set; }

    /// <summary>
    /// عکس RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; set; }
}
