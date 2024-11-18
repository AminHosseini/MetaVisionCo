namespace Api.Features.Shop.Products.Commands.DeleteProduct;

/// <summary>
/// اطلاعات برای حذف محصول
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
    /// تابع مورد استفاده برای رسیدگی به درخواست حذف یک محصول
    /// </summary>
    /// <param name="request">فرمان حذف یک محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی محصول حذف شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.IdRowVersion.Id, cancellationToken)
            ?? throw new RecordNotFoundException();

        _context.Products.Entry(product).SetRowVersionCurrentValue(request.IdRowVersion.RowVersion);
        var isDeleted = _context.Products.Entry(product).Property<bool>(ShadowProperty.IsDeleted).CurrentValue;

        if (isDeleted)
        {
            _context.Products.Entry(product).SetCurrentValue(ShadowProperty.DeleteDate, null);
            _context.Products.Entry(product).SetCurrentValue(ShadowProperty.DeletedByUser, null);
        }
        else
        {
            _context.Products.Entry(product).SetCurrentValue(ShadowProperty.DeleteDate, DateTimeOffset.UtcNow);
            // This must be later replaced with a real user
            _context.Products.Entry(product).SetCurrentValue(ShadowProperty.DeletedByUser, (long)1);
        }

        _context.Products.Entry(product).SetCurrentValue(ShadowProperty.IsDeleted, !isDeleted);
        _context.Products.Attach(product);
        _context.Products.Entry(product).Property(ShadowProperty.IsDeleted).IsModified = true;
        _context.Products.Entry(product).Property(ShadowProperty.DeleteDate).IsModified = true;
        _context.Products.Entry(product).Property(ShadowProperty.DeletedByUser).IsModified = true;
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(product)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(product.Id, rowVersion);
    }
}
