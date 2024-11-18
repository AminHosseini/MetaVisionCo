namespace Api.Features.Shop.Products.Commands.DeleteProduct;

/// <summary>
/// فرمان مورد استفاده برای حذف یا برگرداندن محصول
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// و آیدیRowVersion مقدار
    /// </summary>
    public required IdRowVersion IdRowVersion { get; set; }
}
