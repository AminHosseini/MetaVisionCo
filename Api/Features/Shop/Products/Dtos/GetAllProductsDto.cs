namespace Api.Features.Shop.Products.Dtos;

/// <summary>
/// برای گرفتن اطلاعات محصول ها
/// </summary>
public readonly record struct GetAllProductsDto
{
    /// <summary>
    /// آیدی محصول
    /// </summary>
    public required long ProductId { get; init; }

    /// <summary>
    /// نام دسته بندی محصول
    /// </summary>
    public required string ProductCategoryName { get; init; }

    /// <summary>
    /// نام محصول
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// کد محصولی
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// حذف شده؟
    /// </summary>
    public required bool IsDeleted { get; init; }

    /// <summary>
    /// دسته بندی محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; init; }
}
