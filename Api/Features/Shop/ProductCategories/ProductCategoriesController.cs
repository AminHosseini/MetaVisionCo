namespace Api.Features.Shop.ProductCategories;

/// <summary>
/// برای کار با دسته بندی های محصول
/// </summary>
[EnableQueryWithMetadata]
public class ProductCategoriesController : ApiControllerBase
{
    #region Queries

    /// <summary>
    /// یافتن دسته بندی محصول با آیدی آن
    /// </summary>
    /// <param name="objectId">آیدی دسته بندی محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات یک دسته بندی محصول</returns>
    [HttpGet("product-categories/{objectId:long:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductCategoryDto))]
    public Task<GetProductCategoryDto> GetProductCategoryAsync([Required][FromRoute] long objectId, CancellationToken cancellationToken)
    {
        Queries.GetProductCategory.Query query = new() { ProductCategoryId = objectId };
        return Mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// یافتن دسته بندی های محصول
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات دسته بندی های محصول</returns>
    [HttpGet("product-categories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<GetAllProductCategoriesDto>))]
    public Task<IQueryable<GetAllProductCategoriesDto>> GetAllProductCategoriesAsync(CancellationToken cancellationToken)
    {
        Queries.GetAllProductCategories.Query query = new();
        return Mediator.Send(query, cancellationToken);
    }

    #endregion

    #region Commands

    /// <summary>
    /// ایجاد دسته بندی محصول
    /// </summary>
    /// <param name="createProductCategory">اطلاعات دسته بندی محصول برای ایجاد</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created Id And RowVersion</returns>
    [HttpPost("product-categories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("application/json")]
    public Task<IdRowVersionGet> CreateProductCategoryAsync([Required][FromBody] CreateProductCategoryDto createProductCategory, CancellationToken cancellationToken)
    {
        Commands.CreateProductCategory.Command command = new() { CreateProductCategoryDto = createProductCategory };
        return Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// ویرایش دسته بندی محصول
    /// </summary>
    /// <param name="objectId">productCategoryId</param>
    /// <param name="updateProductCategory">اطلاعات دسته بندی محصول برای ویرایش</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated Id And RowVersion</returns>
    [HttpPut("product-categories/{objectId:long:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("application/json")]
    public Task<IdRowVersionGet> UpdateProductCategoryAsync([Required][FromRoute] long objectId, [Required][FromBody] UpdateProductCategoryDto updateProductCategory, CancellationToken cancellationToken)
    {
        Commands.UpdateProductCategory.Command command = new() { ProductCategoryId = objectId, UpdateProductCategoryDto = updateProductCategory };
        return Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// دسته بندی محصول را حذف میکند و یا برمیگرداند
    /// </summary>
    /// <param name="idRowVersion">اطلاعات دسته بندی محصول که قرار است حذف شود و یا برگردد</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Deleted Id And RowVersion</returns>
    [HttpPatch("product-categories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("application/json")]
    public Task<IdRowVersionGet> DeleteProductCategoryAsync([Required][FromBody] IdRowVersion idRowVersion, CancellationToken cancellationToken)
    {
        Commands.DeleteProductCategory.Command command = new() { IdRowVersion = idRowVersion };
        return Mediator.Send(command, cancellationToken);
    }

    #endregion
}