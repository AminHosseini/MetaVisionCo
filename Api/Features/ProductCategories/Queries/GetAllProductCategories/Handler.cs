namespace Api.Features.ProductCategories.Queries.GetAllProductCategories;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات دسته بندی های محصول
/// </summary>
public class Handler : IRequestHandler<Query, IQueryable<GetAllProductCategoriesDto>>
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
    /// تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات دسته بندی های محصول
    /// </summary>
    /// <param name="request">جستار استخراج اطلاعات دسته بندی های محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات دسته بندی های محصول برای نمایش به کاربر</returns>
    public Task<IQueryable<GetAllProductCategoriesDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var query = _context.ProductCategories.AsNoTracking();

        return Task.FromResult(query.ProjectToType<GetAllProductCategoriesDto>());
    }
}