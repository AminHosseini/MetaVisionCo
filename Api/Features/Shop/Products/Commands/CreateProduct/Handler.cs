namespace Api.Features.Shop.Products.Commands.CreateProduct;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست ایجاد یک محصول
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
    /// تابع مورد استفاده برای رسیدگی به درخواست ایجاد یک محصول
    /// </summary>
    /// <param name="request">فرمان ایجاد یک محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی محصول تازه ساخته شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var product = request.CreateProductDto.Adapt<Product>();
        _context.Products.Entry(product).SetCurrentValue(ShadowProperty.CreationDate, DateTimeOffset.UtcNow);
        // This 1 must later be replaced by a real user
        _context.Products.Entry(product).SetCurrentValue(ShadowProperty.CreatedByUser, (long)1);

        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(product)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(product.Id, rowVersion);
    }
}
