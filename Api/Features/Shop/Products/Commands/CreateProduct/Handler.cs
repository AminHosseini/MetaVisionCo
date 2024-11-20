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

        var rand = new Random();
        string code = $"FAS-{rand.Next(1, 9999)}-PROD-{rand.Next(100000, 999999)}";
        bool exists = await _context.Products.AnyAsync(p => EF.Property<string>(p, ShadowProperty.Code) == code);
        var i = 0;
        if (exists)
        {
            i++;
            if (i == 3)
                throw new RecordNotFoundException();
            code = $"FAS-{rand.Next(1, 9999)}-PROD-{rand.Next(100000, 999999)}";
        }
        _context.Products.Entry(product).SetCurrentValue(ShadowProperty.Code, code);

        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(product)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(product.Id, rowVersion);
    }
}
