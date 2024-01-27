﻿namespace Api.Features.ProductCategories;

/// <summary>
/// تنظیمات نگاشت های دسته بندی محصول
/// </summary>
public static class ProductCategoryMapConfigs
{
    /// <summary>
    /// ثبت پیکربندی نگاشت ها
    /// </summary>
    public static void RegisterMappingConfigurations(this IServiceCollection services)
    {
        // GetProductCategory
        TypeAdapterConfig<ProductCategory, GetProductCategoryDto>
            .ForType()
            .Map(dest => dest.ProductCategoryId, src => src.Id)
            .Map(dest => dest.IsDeleted, src => EF.Property<bool>(src, ShadowProperty.IsDeleted))
            .Map(dest => dest.RowVersion, src => EF.Property<byte[]>(src, ShadowProperty.RowVersion));

        // GetAllProductCategories
        TypeAdapterConfig<ProductCategory, GetAllProductCategoriesDto>
            .ForType()
            .Map(dest => dest.ProductCategoryId, src => src.Id)
            .Map(dest => dest.IsDeleted, src => EF.Property<bool>(src, ShadowProperty.IsDeleted))
            .Map(dest => dest.RowVersion, src => EF.Property<byte[]>(src, ShadowProperty.RowVersion));

        // CreateProductCategory
        TypeAdapterConfig<CreateProductCategoryDto, ProductCategory>
            .ForType()
            .Map(dest => dest.Seo.Slug, src => src.Seo!.Slug!.Slugify())
            .Map(dest => dest.Seo.Keywords, src => src.Seo!.Keywords.HashtagifyList());

        // UpdateProductCategory
        TypeAdapterConfig<UpdateProductCategoryDto, ProductCategory>
            .ForType()
            .Map(dest => dest.Seo.Slug, src => src.Seo!.Slug!.Slugify())
            .Map(dest => dest.Seo.Keywords, src => src.Seo!.Keywords.HashtagifyList());
    }
}