namespace Api.Features.Shop.Products.Dtos;

/// <summary>
/// برای کمک به مپ کردن گرفتن اطلاعات تمامی محصولات
/// </summary>
public readonly record struct GetAllProductsMappingHelperDto
{
    /// <summary>
    /// محصول
    /// </summary>
    public Product Product { get; init; }

    /// <summary>
    /// دسته بندی محصول
    /// </summary>
    public ProductCategory ProductCategory { get; init; }
}
