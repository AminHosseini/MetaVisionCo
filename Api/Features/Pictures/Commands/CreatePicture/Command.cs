namespace Api.Features.Pictures.Commands.CreatePicture;

/// <summary>
/// فرمان مورد استفاده برای ایجاد یک عکس
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// اطلاعات یک عکس برای ایجاد
    /// </summary>
    public CreatePictureDto CreatePictureDto { get; set; } = default!;
}
