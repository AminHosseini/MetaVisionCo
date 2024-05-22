namespace Domain.Models;

/// <summary>
/// ‍پراپرتی های عکس
/// </summary>
public class Picture : BaseEntity, IHaveSoftDelete, IHaveCreationInfo
{
    /// <summary>
    /// آیدی صاحب عکس
    /// </summary>
    public required long ParentId { get; set; }

    /// <summary>
    /// نام عکس
    /// </summary>
    public required string PictureName { get; set; }

    /// <summary>
    /// آلت عکس برای سئو
    /// </summary>
    public required string PictureAlt { get; set; }

    /// <summary>
    /// عنوان عکس برای سئو
    /// </summary>
    public required string PictureTitle { get; set; }

    /// <summary>
    /// ترتیب نمایش
    /// </summary>
    public required int DisplayOrder { get; set; }

    /// <summary>
    /// نوع صاحب عکس
    /// </summary>
    public required PictureType PictureType { get; set; }

    [SetsRequiredMembers]
    public Picture(long id)
    {
        Id = id;
        PictureName = string.Empty;
        PictureAlt = string.Empty;
        PictureTitle = string.Empty;
    }

    [SetsRequiredMembers]
    public Picture()
    {
        PictureName = string.Empty;
        PictureAlt = string.Empty;
        PictureTitle = string.Empty;
    }
}
