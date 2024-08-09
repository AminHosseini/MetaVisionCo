namespace Api.Features.Pictures.Dtos;

/// <summary>
/// اطلاعات عکس
/// </summary>
public readonly record struct PictureInfoDto
{
    /// <summary>
    /// آدرس عکس
    /// </summary>
    public required string PicturePath { get; init; }

    /// <summary>
    /// نام عکس
    /// </summary>
    public required string PictureName { get; init; }

    /// <summary>
    /// سایز عکس
    /// </summary>
    public required long PictureSize { get; init; }
}
