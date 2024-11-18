using Api.Features.Shop.Products.Dtos;

namespace Api.Features.Shop.Products;

/// <summary>
/// تنظیمات نگاشت های محصول
/// </summary>
public static class ProductMapConfigs
{
    /// <summary>
    /// ثبت پیکربندی نگاشت ها
    /// </summary>
    public static void RegisterMappingConfigurations(this IServiceCollection services)
    {
        // CreateProductCategory
        TypeAdapterConfig<CreateProductDto, Product>
            .ForType()
            .Map(dest => dest.Seo.Slug, src => src.Seo!.Slug!.Slugify())
            .Map(dest => dest.Seo.Keywords, src => src.Seo!.Keywords.Hashtagify());

        // UpdateProductCategory
        TypeAdapterConfig<UpdateProductDto, Product>
            .ForType()
            .Map(dest => dest.Seo.Slug, src => src.Seo!.Slug!.Slugify())
            .Map(dest => dest.Seo.MetaDescription, src => src.Seo!.MetaDescription!.Slugify())
            .Map(dest => dest.Seo.Keywords, src => src.Seo!.Keywords.Hashtagify());

        // GetProductCategory
        TypeAdapterConfig<Product, GetProductDto>
            .ForType()
            .Map(dest => dest.ProductId, src => src.Id)
            .Map(dest => dest.Seo.Keywords, src => src.Seo.Keywords.ListHashtags())
            .Map(dest => dest.IsDeleted, src => EF.Property<bool>(src, ShadowProperty.IsDeleted))
            .Map(dest => dest.RowVersion, src => EF.Property<byte[]>(src, ShadowProperty.RowVersion));

        // GetAllProductCategories
        TypeAdapterConfig<GetAllProductsMappingHelperDto, GetAllProductsDto>
            .ForType()
            .Map(dest => dest.ProductId, src => src.Product.Id)
            .Map(dest => dest.ProductCategoryName, src => src.ProductCategory.Name)
            .Map(dest => dest.Name, src => src.Product.Name)
            .Map(dest => dest.Code, src => src.Product.Code)
            .Map(dest => dest.IsDeleted, src => EF.Property<bool>(src.Product, ShadowProperty.IsDeleted))
            .Map(dest => dest.RowVersion, src => EF.Property<byte[]>(src.Product, ShadowProperty.RowVersion));
    }
}
