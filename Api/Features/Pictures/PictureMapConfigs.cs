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
            .Map(dest => dest.PictureName, src => src.PictureFile!.HandlePicture(src.PictureType, src.ParentId));

        // GetPicturesByParentId
        TypeAdapterConfig<Picture, GetPicturesByParentIdDto>
            .ForType()
            .Map(dest => dest.PictureId, src => src.Id)
            .Map(dest => dest.PictureInfo, src => src.PictureName.GetPicture(src.PictureType, src.ParentId))
            .Map(dest => dest.RowVersion, src => EF.Property<byte[]>(src, ShadowProperty.RowVersion));
    }
}
