namespace Api.Features.ProductCategories.Dtos;

/// <summary>
/// اطلاعات برای ویرایش یک دسته بندی محصول
/// </summary>
public record class UpdateProductCategoryDto : CreateProductCategoryDto
{
    /// <summary>
    /// آیدی نوع محصول
    /// </summary>
    public required long Id { get; set; }

    /// <summary>
    /// دسته بندی محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; set; }
}