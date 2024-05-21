namespace Api.Features.Pictures.Commands.CreatePicture;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست ایجاد یک عکس
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
    /// تابع مورد استفاده برای رسیدگی به درخواست ایجاد یک عکس
    /// </summary>
    /// <param name="request">فرمان ایجاد یک عکس</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی عکس تازه ساخته شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var picture = request.CreatePictureDto.Adapt<Picture>();
        _context.Pictures.Entry(picture).SetCurrentValue(ShadowProperty.CreationDate, DateTimeOffset.UtcNow);
        // This 1 must later be replaced by a real user
        _context.Pictures.Entry(picture).SetCurrentValue(ShadowProperty.CreatedByUser, (long)1);

        await _context.Pictures.AddAsync(picture, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(picture)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(picture.Id, rowVersion);
    }
}