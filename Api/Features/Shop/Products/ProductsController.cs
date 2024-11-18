using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products;

/// <summary>
/// برای کار با محصولات
/// </summary>
[EnableQueryWithMetadata]
public class ProductsController : ApiControllerBase
{
    #region Queries

    /// <summary>
    /// یافتن محصول با آیدی آن
    /// </summary>
    /// <param name="objectId">آیدی محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات یک محصول</returns>
    [HttpGet("products/{objectId:long:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductDto))]
    public Task<GetProductDto> GetProductAsync([Required][FromRoute] long objectId, CancellationToken cancellationToken)
    {
        Queries.GetProduct.Query query = new() { ProductId = objectId };
        return Mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// یافتن محصول ها
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات محصول ها</returns>
    [HttpGet("products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<GetAllProductsDto>))]
    public Task<IQueryable<GetAllProductsDto>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        Queries.GetAllProducts.Query query = new();
        return Mediator.Send(query, cancellationToken);
    }

    #endregion

    #region Commands

    /// <summary>
    /// ایجاد محصول
    /// </summary>
    /// <param name="createProduct">اطلاعات محصول برای ایجاد</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created Id And RowVersion</returns>
    [HttpPost("products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("application/json")]
    public Task<IdRowVersionGet> CreateProductAsync([Required][FromBody] CreateProductDto createProduct, CancellationToken cancellationToken)
    {
        Commands.CreateProduct.Command command = new() { CreateProductDto = createProduct };
        return Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// ویرایش محصول
    /// </summary>
    /// <param name="objectId">productId</param>
    /// <param name="updateProduct">اطلاعات محصول برای ویرایش</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated Id And RowVersion</returns>
    [HttpPut("products/{objectId:long:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("application/json")]
    public Task<IdRowVersionGet> UpdateProductAsync([Required][FromRoute] long objectId, [Required][FromBody] UpdateProductDto updateProduct, CancellationToken cancellationToken)
    {
        Commands.UpdateProduct.Command command = new() { ProductId = objectId, UpdateProductDto = updateProduct };
        return Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// محصول را حذف میکند و یا برمیگرداند
    /// </summary>
    /// <param name="idRowVersion">اطلاعات محصول که قرار است حذف شود و یا برگردد</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Deleted Id And RowVersion</returns>
    [HttpPatch("products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("application/json")]
    public Task<IdRowVersionGet> DeleteProductAsync([Required][FromBody] IdRowVersion idRowVersion, CancellationToken cancellationToken)
    {
        Commands.DeleteProduct.Command command = new() { IdRowVersion = idRowVersion };
        return Mediator.Send(command, cancellationToken);
    }

    #endregion
}
