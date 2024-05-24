namespace Api.Features.Pictures.Commands.UpdatePicture;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست ویرایش یک عکس
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
    /// تابع مورد استفاده برای رسیدگی به درخواست ویرایش یک عکس
    /// </summary>
    /// <param name="request">فرمان ویرایش یک عکس</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی عکس ویرایش شده RowVersion</returns>
    public async Task<IdRowVersionGet> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var picture = await _context.Pictures
            .FirstOrDefaultAsync(pc => pc.Id == request.PictureId, cancellationToken)
            ?? throw new RecordNotFoundException();

        _context.Pictures.Entry(picture).SetRowVersionCurrentValue(request.UpdatePictureDto.RowVersion);

        picture.PictureAlt = request.UpdatePictureDto.PictureAlt!;
        picture.PictureTitle = request.UpdatePictureDto.PictureTitle!;
        picture.DisplayOrder = request.UpdatePictureDto.DisplayOrder;

        _context.Pictures.Attach(picture);

        _context.Pictures.Entry(picture).Property(p => p.PictureAlt).IsModified = true;
        _context.Pictures.Entry(picture).Property(p => p.PictureTitle).IsModified = true;
        _context.Pictures.Entry(picture).Property(p => p.DisplayOrder).IsModified = true;

        await _context.SaveChangesAsync(cancellationToken);

        var rowVersion = _context.Entry(picture)
            .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        return new IdRowVersionGet(picture.Id, rowVersion);
    }
}
