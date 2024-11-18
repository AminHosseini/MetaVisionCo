using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products.Commands.UpdateProduct;

/// <summary>
/// فرمان مورد استفاده برای ویرایش یک محصول
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// آیدی یک محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// اطلاعات یک محصول برای ویرایش
    /// </summary>
    public UpdateProductDto UpdateProductDto { get; set; } = default!;
}
