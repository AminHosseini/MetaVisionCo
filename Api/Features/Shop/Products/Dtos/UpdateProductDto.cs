namespace Api.Features.Shop.Products.Dtos;

/// <summary>
/// اطلاعات برای ویرایش یک محصول
/// </summary>
public record class UpdateProductDto : CreateProductDto
{
    /// <summary>
    /// محصول RowVersion مقدار
    /// </summary>
    public required RowVersionValue RowVersion { get; set; }
}
