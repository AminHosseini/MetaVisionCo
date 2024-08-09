namespace Api.Features.Pictures.Queries.GetPictureByParentId;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات عکس های صاحب عکس
/// </summary>
public class Handler : IRequestHandler<Query, IQueryable<GetPicturesByParentIdDto>>
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
    /// تابع مورد استفاده برای رسیدگی به درخواست استخراج اطلاعات عکس های صاحب عکس
    /// </summary>
    /// <param name="request">جستار استخراج اطلاعات عکس های صاحب عکس</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات عکس های صاحب عکس برای نمایش به کاربر</returns>
    public Task<IQueryable<GetPicturesByParentIdDto>> Handle(Query request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var query = _context.Pictures.AsNoTracking()
            .Where(p => p.ParentId == request.ParentId && p.PictureType == request.PictureType);

        //if (!query.Any())
        //    throw new RecordNotFoundException();

        
        
        
        return Task.FromResult(query.ProjectToType<GetPicturesByParentIdDto>());
    }
}
