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

        // GetPicturesByParentId
        TypeAdapterConfig<Picture, GetPicturesByParentIdDto>
            .ForType()
            .Map(dest => dest.PictureId, src => src.Id)
            .Map(dest => dest.PicturePath, src => $"{Path.Combine(Directory.GetCurrentDirectory(), FilePath.MainPath, src.PictureName)}")
            .Map(dest => dest.IsDeleted, src => EF.Property<bool>(src, ShadowProperty.IsDeleted))
            .Map(dest => dest.RowVersion, src => EF.Property<byte[]>(src, ShadowProperty.RowVersion));
    }
}
