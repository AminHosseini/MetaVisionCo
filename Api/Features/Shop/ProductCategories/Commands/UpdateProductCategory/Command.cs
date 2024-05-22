namespace Api.Features.Shop.ProductCategories.Commands.UpdateProductCategory;

/// <summary>
/// فرمان مورد استفاده برای ویرایش یک دسته بندی محصول
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// آیدی یک دسته بندی محصول
    /// </summary>
    public long ProductCategoryId { get; set; }

    /// <summary>
    /// اطلاعات یک دسته بندی محصول برای ویرایش
    /// </summary>
    public UpdateProductCategoryDto UpdateProductCategoryDto { get; set; } = default!;
}