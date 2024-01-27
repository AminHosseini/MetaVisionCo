namespace Api.Features.ProductCategories.Commands.DeleteProductCategory;

/// <summary>
/// اطلاعات برای حذف دسته بندی محصول
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
    /// تابع مورد استفاده برای رسیدگی به درخواست حذف یک دسته بندی محصول
    /// </summary>
    /// <param name="request">فرمان حذف یک دسته بندی محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی دسته بندی محصول حذف شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var productCategory = new ProductCategory(request.IdRowVersion.Id);
        _context.ProductCategories.Entry(productCategory).SetRowVersionCurrentValue(request.IdRowVersion.RowVersion);

        var currentState = _context.ProductCategories.Entry(productCategory).Property<bool>(ShadowProperty.IsDeleted).CurrentValue;
        _context.ProductCategories.Entry(productCategory).SetCurrentValue(ShadowProperty.IsDeleted, !currentState);

        _context.ProductCategories.Attach(productCategory);
        _context.ProductCategories.Entry(productCategory).Property(ShadowProperty.IsDeleted).IsModified = true;
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(productCategory)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(productCategory.Id, rowVersion);
    }
}