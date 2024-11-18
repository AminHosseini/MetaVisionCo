namespace Api.Features.Shop.ProductCategories.Dtos;

/// <summary>
/// برای گرفتن اطلاعات دسته بندی های محصول
/// </summary>
public record class GetAllProductCategoriesDto
{
    /// <summary>
    /// آیدی دسته بندی محصول
    /// </summary>
    public required long ProductCategoryId { get; set; }

    /// <summary>
    /// آیدی دسته بندی اصلی محصول
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// حذف شده؟
    /// </summary>
    public required bool IsDeleted { get; set; }

    /// <summary>
    /// دسته بندی محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; set; }

    /// <summary>
    /// زیرمجموعه دسته بندی های محصول
    /// </summary>
    public IEnumerable<GetAllProductCategoriesDto>? Children { get; set; }
}