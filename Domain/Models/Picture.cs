//namespace Domain.Models;

///// <summary>
///// ‍پراپرتی های عکس
///// </summary>
//public class Picture : BaseEntity, ICanDisable
//{
//    /// <summary>
//    /// آیدی موجودیت مادر
//    /// </summary>
//    public required long ParentId { get; set; }

//    /// <summary>
//    /// آدرس عکس
//    /// </summary>
//    public required string PicturePath { get; set; }

//    /// <summary>
//    /// آلت عکس برای سئو
//    /// </summary>
//    public required string PictureAlt { get; set; }

//    /// <summary>
//    /// عنوان عکس برای سئو
//    /// </summary>
//    public required string PictureTitle { get; set; }

//    /// <summary>
//    /// عکس اصلی است؟
//    /// </summary>
//    public required bool IsDefault { get; set; }

//    // PictureType Shadow Property

//    [SetsRequiredMembers]
//    public Picture()
//    {
//        PicturePath = string.Empty;
//        PictureAlt = string.Empty;
//        PictureTitle = string.Empty;
//    }
//}
