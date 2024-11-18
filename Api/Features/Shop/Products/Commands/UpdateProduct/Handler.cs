namespace Api.Features.Shop.Products.Commands.UpdateProduct;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست ویرایش یک محصول
/// </summary>
public class Handler : IRequestHandler<Command, IdRowVersionGet>
{
    /// <summary>
    /// زمینه پایگاه داده برنامه
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// سازنده کلاس
    /// </summary>
    /// <param name="context">زمینه پایگاه داده برنامه</param>
    public Handler(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    /// <summary>
    /// تابع مورد استفاده برای رسیدگی به درخواست ویرایش یک محصول
    /// </summary>
    /// <param name="request">فرمان ویرایش یک محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی محصول ویرایش شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var product = request.UpdateProductDto.Adapt<Product>();
        product.Id = request.ProductId;

        _context.Products.Entry(product).SetRowVersionCurrentValue(request.UpdateProductDto.RowVersion);
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(product)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(product.Id, rowVersion);
    }
}
