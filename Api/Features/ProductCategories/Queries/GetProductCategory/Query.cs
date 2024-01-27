namespace Api.Features.ProductCategories.Queries.GetProductCategory;

/// <summary>
/// جستار مورد استفاده در استخراج اطلاعات یک دسته بندی محصول
/// </summary>
public class Query : IRequest<GetProductCategoryDto>
{
    /// <summary>
    /// آیدی دسته بندی محصول
    /// </summary>
    public required long ProductCategoryId { get; set; }
}