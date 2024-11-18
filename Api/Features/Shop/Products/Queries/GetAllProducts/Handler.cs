using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products.Queries.GetAllProducts;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات محصول ها
/// </summary>
public class Handler : IRequestHandler<Query, IQueryable<GetAllProductsDto>>
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
    /// تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات محصول ها
    /// </summary>
    /// <param name="request">جستار استخراج اطلاعات محصول ها</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات محصول ها برای نمایش به کاربر</returns>
    public Task<IQueryable<GetAllProductsDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var query = _context.Products.AsNoTracking()
            .Join(_context.ProductCategories.AsNoTracking(),
                product => product.ProductCategoryId,
                productCategory => productCategory.Id,
                (product, productCategory) => new GetAllProductsMappingHelperDto { Product = product, ProductCategory = productCategory });

        //var query = from product in _context.Products.AsNoTracking()
        //            join productCategory in _context.ProductCategories.AsNoTracking()
        //            on product.ProductCategoryId equals productCategory.Id
        //            select new GetAllProductsMappingHelperDto { Product = product, ProductCategory = productCategory };

        return Task.FromResult(query.ProjectToType<GetAllProductsDto>());
    }
}
