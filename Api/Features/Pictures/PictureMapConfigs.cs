namespace Api.Features.Pictures;

/// <summary>
/// تنظیمات نگاشت های عکس
/// </summary>
public static class PictureMapConfigs
{
    /// <summary>
    /// ثبت پیکربندی نگاشت ها
    /// </summary>
    public static void RegisterMappingConfigurations(this IServiceCollection services)
    {
        // CreatePictures
        TypeAdapterConfig<CreatePictureDto, Picture>
            .ForType()
            .Map(dest => dest.PictureName, src => src.PictureName!.HandleFile(src.ParentSlug));
    }
}
