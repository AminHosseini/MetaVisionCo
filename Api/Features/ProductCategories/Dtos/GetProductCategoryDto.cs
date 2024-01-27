namespace Api.Features.ProductCategories.Dtos;

/// <summary>
/// برای گرفتن اطلاعات یک دسته بندی محصول
/// </summary>
public readonly record struct GetProductCategoryDto
{
    /// <summary>
    /// آیدی دسته بندی محصول
    /// </summary>
    public required long ProductCategoryId { get; init; }

    /// <summary>
    /// حذف شده؟
    /// </summary>
    public required bool IsDeleted { get; init; }

    /// <summary>
    /// شماره سریال دسته بندی محصول
    /// </summary>
    public required Guid SerialNumber { get; init; }

    /// <summary>
    /// دسته بندی محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; init; }

    /// <summary>
    /// آیدی دسته بندی اصلی محصول
    /// </summary>
    public long? ParentId { get; init; }

    /// <summary>
    /// عنوان
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// توضیحات
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// ‍پراپرتی های سئو
    /// </summary>
    public SeoDto Seo { get; init; }
}