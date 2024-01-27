namespace Api.Features.ProductCategories.Commands.DeleteProductCategory;

/// <summary>
/// فرمان مورد استفاده برای حذف یا برگرداندن دسته بندی محصول
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// و آیدیRowVersion مقدار
    /// </summary>
    public required IdRowVersion IdRowVersion { get; set; }
}