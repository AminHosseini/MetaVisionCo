namespace Api.Features.Pictures.Dtos;

/// <summary>
/// برای گرفتن اطلاعات عکس های صاحب عکس
/// </summary>
public readonly record struct GetPicturesByParentIdDto
{
    /// <summary>
    /// آیدی صاحب عکس
    /// </summary>
    public required long ParentId { get; init; }

    /// <summary>
    /// آیدی عکس
    /// </summary>
    public required long PictureId { get; init; }

    /// <summary>
    /// عکس RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; init; }

    /// <summary>
    /// آدرس عکس
    /// </summary>
    public required string PicturePath { get; init; }

    /// <summary>
    /// آلت عکس برای سئو
    /// </summary>
    public required string PictureAlt { get; init; }

    /// <summary>
    /// عنوان عکس برای سئو
    /// </summary>
    public required string PictureTitle { get; init; }

    /// <summary>
    /// ترتیب نمایش
    /// </summary>
    public required int DisplayOrder { get; init; }

    /// <summary>
    /// نوع صاحب عکس
    /// </summary>
    public required PictureType PictureType { get; init; }
}
