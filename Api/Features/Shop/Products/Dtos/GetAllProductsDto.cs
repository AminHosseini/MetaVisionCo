namespace Api.Features.Shop.Products.Dtos;

/// <summary>
/// برای گرفتن اطلاعات محصول ها
/// </summary>
public record class GetAllProductsDto
{
    /// <summary>
    /// آیدی محصول
    /// </summary>
    public required long ProductId { get; set; }

    /// <summary>
    /// نام دسته بندی محصول
    /// </summary>
    public required string ProductCategoryName { get; set; }

    /// <summary>
    /// نام محصول
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// کد محصولی
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// حذف شده؟
    /// </summary>
    public required bool IsDeleted { get; set; }

    /// <summary>
    /// دسته بندی محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; set; }
}
