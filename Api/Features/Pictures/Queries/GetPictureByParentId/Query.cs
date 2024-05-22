namespace Api.Features.Pictures.Queries.GetPictureByParentId;

/// <summary>
/// جستار مورد استفاده در استخراج اطلاعات عکس های صاحب عکس
/// </summary>
public class Query : IRequest<IQueryable<GetPicturesByParentIdDto>>
{
    /// <summary>
    /// آیدی صاحب عکس
    /// </summary>
    public required long ParentId { get; set; }

    /// <summary>
    /// نوع صاحب عکس
    /// </summary>
    public required PictureType PictureType { get; set; }
}
