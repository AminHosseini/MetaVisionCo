namespace Api.Features.Shop.ProductCategories.Dtos;

/// <summary>
/// کمک به مپ کردن گرفتن اطلاعات تمامی دسته بندی های محصول
/// </summary>
public readonly record struct GetAllProductCategoriesMappingHelperDto
{
    /// <summary>
    /// دسته بندی محصول اصلی
    /// </summary>
    public ProductCategory Parent { get; init; }

    /// <summary>
    /// دسته بندی های محصول وابسته
    /// </summary>
    public IEnumerable<ProductCategory> Children { get; init; }
}
