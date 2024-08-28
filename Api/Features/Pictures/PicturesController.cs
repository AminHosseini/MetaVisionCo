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
    /// <param name="parentId">آیدی صاحب عکس</param>
    /// <param name="pictureType">نوع صاحب عکس</param>
    /// <param name="cancellationToken"></param>
    /// <returns>اطلاعات عکس های صاحب عکس</returns>
    [HttpGet("pictures")]
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
    /// <param name="createPictureDto">اطلاعات عکس برای ایجاد</param>
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

    /// <summary>
    /// ویرایش عکس
    /// </summary>
    /// <param name="objectId">pictureId</param>
    /// <param name="updatePicture">اطلاعات عکس برای ویرایش</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated Id And RowVersion</returns>
    [HttpPut("pictures/{objectId:long:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdRowVersionGet))]
    [Consumes("application/json")]
    public Task<IdRowVersionGet> UpdatePictureAsync([FromRoute][Required] long objectId, [FromBody][Required] UpdatePictureDto updatePicture, CancellationToken cancellationToken)
    {
        Commands.UpdatePicture.Command command = new() { PictureId = objectId, UpdatePictureDto = updatePicture };
        return Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// ویرایش ترتیب نمایش عکس ها
    /// </summary>
    /// <param name="changePicturesOrders">اطلاعات برای ویرایش ترتیب نمایش عکس ها</param>
    /// <param name="cancellationToken"></param>
    /// <returns>عکس های ویرایش شده RowVersion لیستی از آیدی و</returns>
    [HttpPut("pictures/order")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IdRowVersionGet>))]
    [Consumes("application/json")]
    public Task<IEnumerable<IdRowVersionGet>> ChangePicturesOrderAsync([FromBody][Required] IEnumerable<ChangePicturesOrderDto> changePicturesOrders, CancellationToken cancellationToken)
    {
        Commands.ChangePicturesOrder.Command command = new() { ChangePicturesOrderDtos = changePicturesOrders };
        return Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// عکس را حذف میکند
    /// </summary>
    /// <param name="idRowVersion">اطلاعات عکس که قرار است حذف شود</param>
    /// <param name="cancellationToken"></param>
    /// <returns>آیا عملیات حذف موفق بود؟</returns>
    [HttpPatch("pictures")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [Consumes("application/json")]
    public Task<bool> DeletePictureAsync([FromBody][Required] IdRowVersion idRowVersion, CancellationToken cancellationToken)
    {
        Commands.DeletePicture.Command command = new() { IdRowVersion = idRowVersion };
        return Mediator.Send(command, cancellationToken);
    }

    #endregion
}
