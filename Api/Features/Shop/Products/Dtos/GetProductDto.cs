namespace Api.Features.Shop.Products.Dtos;

/// <summary>
/// برای گرفتن اطلاعات یک محصول
/// </summary>
public readonly record struct GetProductDto
{
    /// <summary>
    /// آیدی محصول
    /// </summary>
    public required long ProductId { get; init; }

    /// <summary>
    /// آیدی دسته بندی محصول
    /// </summary>
    public required long ProductCategoryId { get; init; }

    /// <summary>
    /// عنوان
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// کد محصولی
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// توضیحات کوتاه
    /// </summary>
    public required string ShortDescription { get; init; }

    /// <summary>
    /// توضیحات
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// ‍پراپرتی های سئو
    /// </summary>
    public SeoDto Seo { get; init; }

    /// <summary>
    /// حذف شده؟
    /// </summary>
    public required bool IsDeleted { get; init; }

    /// <summary>
    /// محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; init; }
}
