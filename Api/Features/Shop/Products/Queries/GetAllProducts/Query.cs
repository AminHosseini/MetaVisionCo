using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products.Queries.GetAllProducts;

/// <summary>
/// جستار مورد استفاده در استخراج اطلاعات محصول ها
/// </summary>
public class Query : IRequest<IQueryable<GetAllProductsDto>>
{
}
