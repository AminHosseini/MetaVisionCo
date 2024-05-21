namespace Api.Features.ProductCategories.Commands.CreateProductCategory;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست ایجاد یک دسته بندی محصول
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
    /// تابع مورد استفاده برای رسیدگی به درخواست ایجاد یک دسته بندی محصول
    /// </summary>
    /// <param name="request">فرمان ایجاد یک دسته بندی محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی دسته بندی محصول تازه ساخته شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var productCategory = request.CreateProductCategoryDto.Adapt<ProductCategory>();
        _context.ProductCategories.Entry(productCategory).SetCurrentValue(ShadowProperty.CreationDate, DateTimeOffset.UtcNow);
        // This 1 must later be replaced by a real user
        _context.ProductCategories.Entry(productCategory).SetCurrentValue(ShadowProperty.CreatedByUser, (long)1);

        await _context.ProductCategories.AddAsync(productCategory, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(productCategory)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(productCategory.Id, rowVersion);
    }
}