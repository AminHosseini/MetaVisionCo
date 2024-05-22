namespace Api.Features.Pictures.Commands.UpdatePicture;

/// <summary>
/// فرمان مورد استفاده برای ویرایش یک عکس
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// آیدی یک عکس
    /// </summary>
    public long PictureId { get; set; }

    /// <summary>
    /// اطلاعات یک عکس برای ویرایش
    /// </summary>
    public UpdatePictureDto UpdatePictureDto { get; set; } = default!;
}
