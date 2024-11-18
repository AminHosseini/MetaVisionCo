using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products.Commands.CreateProduct;

/// <summary>
/// فرمان مورد استفاده برای ایجاد یک محصول
/// </summary>
public class Command : IRequest<IdRowVersionGet>
{
    /// <summary>
    /// اطلاعات یک محصول برای ایجاد
    /// </summary>
    public CreateProductDto CreateProductDto { get; set; } = default!;
}
