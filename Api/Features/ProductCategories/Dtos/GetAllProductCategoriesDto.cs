namespace Api.Features.ProductCategories.Dtos;

/// <summary>
/// برای گرفتن اطلاعات دسته بندی های محصول
/// </summary>
public readonly record struct GetAllProductCategoriesDto
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
}