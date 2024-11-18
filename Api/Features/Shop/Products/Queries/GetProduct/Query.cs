using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products.Queries.GetProduct;

/// <summary>
/// جستار مورد استفاده در استخراج اطلاعات یک محصول
/// </summary>
public class Query : IRequest<GetProductDto>
{
    /// <summary>
    /// آیدی محصول
    /// </summary>
    public required long ProductId { get; set; }
}
