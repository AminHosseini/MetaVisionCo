using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace DataAccess.EntityTypeConfigurations;

/// <summary>
/// کلاس پیکربندی موجودیت های دسته بندی محصول
/// </summary>
public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    /// <summary>
    /// تنظیمات موجودیت های دسته بندی محصول
    /// </summary>
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder = builder.ToTable(
            t => t.HasComment("دسته بندی های محصول")
        );

        builder.Property(pc => pc.ParentId)
            .IsRequired(false)
            .HasComment("آیدی دسته بندی اصلی محصول");

        builder.Property(pc => pc.Name)
            .HasMaxLength(50)
            .IsRequired()
            .HasComment("عنوان");

        builder.Property(pc => pc.Description)
            .HasMaxLength(1000)
            .IsRequired()
            .HasComment("توضیحات");

        builder = builder.OwnsOne(pc => pc.Picture, nb =>
        {
            nb.Property(p => p.PicturePath)
                .HasMaxLength(500)
                .IsRequired()
                .HasComment("آدرس عکس");

            nb.Property(p => p.PictureAlt)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasComment("آلت عکس برای سئو");

            nb.Property(p => p.PictureTitle)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasComment("عنوان عکس برای سئو");
        });

        builder = builder.OwnsOne(pc => pc.Seo, nb =>
        {
            nb.Property(s => s.Slug)
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("اسلاگ برای سئو");

            nb.Property(s => s.Keywords)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasComment("کلمات کلیدی برای سئو");

            nb.Property(s => s.MetaDescription)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasComment("توضیحات متا برای سئو");
        });
    }
}
