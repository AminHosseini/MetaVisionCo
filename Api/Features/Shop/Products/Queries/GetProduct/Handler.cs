using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products.Queries.GetProduct;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات یک محصول
/// </summary>
public class Handler : IRequestHandler<Query, GetProductDto>
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
    /// تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات یک محصول
    /// </summary>
    /// <param name="request">جستار استخراج اطلاعات یک محصول</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات یک محصول برای نمایش به کاربر</returns>
    public async Task<GetProductDto> Handle(Query request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var query = _context.Products.AsNoTracking()
            .Where(p => p.Id == request.ProductId);

        if (!query.Any())
            throw new RecordNotFoundException();

        return await query.ProjectToType<GetProductDto>().FirstOrDefaultAsync(cancellationToken);
    }
}
