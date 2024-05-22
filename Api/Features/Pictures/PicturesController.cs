namespace Api.Features.Pictures;

/// <summary>
/// برای کار با عکس ها
/// </summary>
[EnableQueryWithMetadata]
public class PicturesController : ApiControllerBase
{
    #region Queries

    /// <summary>
    /// یافتن عکس های صاحب عکس
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات عکس های صاحب عکس</returns>
    [HttpGet("picture")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<GetPicturesByParentIdDto>))]
    public Task<IQueryable<GetPicturesByParentIdDto>> GetPicturesByParentIdAsync([Required] long parentId, [Required] PictureType pictureType, CancellationToken cancellationToken)
    {
        Queries.GetPictureByParentId.Query query = new() { ParentId = parentId, PictureType = pictureType };
        return Mediator.Send(query, cancellationToken);
    }

    #endregion

    #region Commands

    /// <summary>
    /// ایجاد عکس
    /// </summary>
    /// <param name="createProductCategory">اطلاعات عکس برای ایجاد</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created Id And RowVersion</returns>
    [HttpPost("pictures")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("multipart/form-data")]
    public Task<IdRowVersionGet> CreatePictureAsync([FromForm][Required] CreatePictureDto createPictureDto, CancellationToken cancellationToken)
    {
        Commands.CreatePicture.Command command = new() { CreatePictureDto = createPictureDto };
        return Mediator.Send(command, cancellationToken);
    }

    #endregion
}
