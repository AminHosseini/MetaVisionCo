namespace Api.Features.Shop.ProductCategories.Commands.UpdateProductCategory;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست ویرایش یک دسته بندی محصول
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
    /// تابع مورد استفاده برای رسیدگی به درخواست ویرایش یک دسته بندی محصول
    /// </summary>
    /// <param name="request">فرمان ویرایش یک دسته بندی محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی دسته بندی محصول ویرایش شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var productCategory = request.UpdateProductCategoryDto.Adapt<ProductCategory>();
        productCategory.Id = request.ProductCategoryId;

        var productCategoryData = await _context.ProductCategories.AsNoTracking()
            .FirstOrDefaultAsync(pc => pc.Id == productCategory.Id, cancellationToken)
            ?? throw new RecordNotFoundException();

        _context.Entry(productCategory).SetRowVersionCurrentValue(request.UpdateProductCategoryDto.RowVersion);
        _context.ProductCategories.Update(productCategory);
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(productCategory)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(productCategory.Id, rowVersion);
    }
}