namespace Api.Features.Pictures.Commands.DeletePicture;

/// <summary>
/// فرمان مورد استفاده برای حذف عکس
/// </summary>
public class Command : IRequest<bool>
{
    /// <summary>
    /// و آیدیRowVersion مقدار
    /// </summary>
    public required IdRowVersion IdRowVersion { get; set; }
}
