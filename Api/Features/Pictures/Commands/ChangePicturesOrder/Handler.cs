namespace Api.Features.Pictures.Commands.ChangePicturesOrder;

/// <summary>
/// کلاس شامل تابع مورد استفاده برای رسیدگی به درخواست ویرایش ترتیب نمایش عکس ها
/// </summary>
public class Handler : IRequestHandler<Command, IEnumerable<IdRowVersionGet>>
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
    /// تابع مورد استفاده برای رسیدگی به درخواست ویرایش ترتیب نمایش عکس ها
    /// </summary>
    /// <param name="request">فرمان ویرایش ترتیب نمایش عکس ها</param>
    /// <param name="cancellationToken"></param>
    /// <returns>و آیدی عکس های ویرایش شده RowVersion</returns>
    public async Task<IEnumerable<IdRowVersionGet>> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RecordNotFoundException();

        var changedPictures = new List<Picture>();
        foreach (var changePicturesOrder in request.ChangePicturesOrderDtos)
        {
            //var picture = await _context.Pictures
            //    .FirstOrDefaultAsync(p => p.Id == changePicturesOrder.PictureId && p.PictureType == changePicturesOrder.PictureType, cancellationToken)
            //    ?? throw new RecordNotFoundException();
            var picture = new Picture(changePicturesOrder.PictureId);
            _context.Pictures.Entry(picture).SetRowVersionCurrentValue(changePicturesOrder.RowVersion);
            picture.DisplayOrder = changePicturesOrder.DisplayOrder;

            _context.Pictures.Attach(picture);
            _context.Pictures.Entry(picture).Property(p => p.DisplayOrder).IsModified = true;
            changedPictures.Add(picture);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var idRowVersions = new List<IdRowVersionGet>();
        foreach (var picture in changedPictures)
        {
            var rowVersion = _context.Entry(picture)
                .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
            idRowVersions.Add(new IdRowVersionGet(picture.Id, rowVersion));
        }

        //var rowVersion = _context.Entry(picture)
        //    .Property<byte[]>(ShadowProperty.RowVersion).CurrentValue;
        //list.Add(new IdRowVersionGet(picture.Id, rowVersion));

        return idRowVersions.AsEnumerable();
    }
}
