namespace Api.Features.Pictures.Commands.ChangePicturesOrder;

/// <summary>
/// فرمان مورد استفاده برای ویرایش ترتیب نمایش عکس ها
/// </summary>
public class Command : IRequest<IEnumerable<IdRowVersionGet>>
{
    /// <summary>
    /// اطلاعات عکس ها برای ویرایش ترتیب اولویت
    /// </summary>
    public IEnumerable<ChangePicturesOrderDto> ChangePicturesOrderDtos { get; set; } = default!;
}
