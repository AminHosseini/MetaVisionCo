namespace Api.Features.Shop.ProductCategories.Commands.CreateProductCategory;

/// <summary>
/// فرمان مورد استفاده برای ایجاد یک دسته بندی محصول
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// اطلاعات یک دسته بندی محصول برای ایجاد
    /// </summary>
    public CreateProductCategoryDto CreateProductCategoryDto { get; set; } = default!;
}