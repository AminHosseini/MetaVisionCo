namespace Api.Features.Pictures.Commands.DeletePicture;

/// <summary>
/// اطلاعات برای حذف عکس
/// </summary>
public class Handler : IRequestHandler<Command, bool>
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
    /// تابع مورد استفاده برای رسیدگی به درخواست حذف یک عکس
    /// </summary>
    /// <param name="request">فرمان حذف یک عکس</param>
    /// <param name="cancellationToken"></param>
    /// <returns>آیا عملیات حذف موفق بود؟</returns>
    public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
    {
        try
        {
            if (request is null)
                throw new RecordNotFoundException();

            var picture = await _context.Pictures
                .FirstOrDefaultAsync(p => p.Id == request.IdRowVersion.Id, cancellationToken);

            if (picture is null)
                throw new RecordNotFoundException();

            bool result = picture.PictureName.DeleteFile();
            if (!result)
                throw new Exception();

            _context.Pictures.Attach(picture);
            _context.Pictures.Remove(picture);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}