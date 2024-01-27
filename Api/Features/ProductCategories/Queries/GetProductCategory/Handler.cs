namespace Api.Features.ProductCategories.Queries.GetProductCategory;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات یک دسته بندی محصول
/// </summary>
public class Handler : IRequestHandler<Query, GetProductCategoryDto>
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
    /// تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات یک دسته بندی محصول
    /// </summary>
    /// <param name="request">جستار استخراج اطلاعات یک دسته بندی محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات یک دسته بندی محصول برای نمایش به کاربر</returns>
    public async Task<GetProductCategoryDto> Handle(Query request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var query = await _context.ProductCategories.AsNoTracking()
            .FirstOrDefaultAsync(pc => pc.Id == request.ProductCategoryId
                && !EF.Property<bool>(pc, ShadowProperty.IsDeleted), cancellationToken);

        return query.Adapt<GetProductCategoryDto>();
    }
}