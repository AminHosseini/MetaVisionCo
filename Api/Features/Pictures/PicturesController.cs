namespace Api.Features.Pictures;

/// <summary>
/// برای کار با عکس ها
/// </summary>
[EnableQueryWithMetadata]
public class PicturesController : ApiControllerBase
{
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
}
