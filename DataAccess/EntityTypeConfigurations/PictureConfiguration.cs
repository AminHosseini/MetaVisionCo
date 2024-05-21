namespace DataAccess.EntityTypeConfigurations;

/// <summary>
/// کلاس پیکربندی موجودیت های عکس
/// </summary>
public class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    /// <summary>
    /// تنظیمات موجودیت های عکس
    /// </summary>
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder = builder.ToTable("Pictures", "Main",
            t => t.HasComment("عکس ها")
        );

        builder.Property(p => p.ParentId)
            .IsRequired()
            .HasComment("آیدی موجودیت مادر");

        builder.Property(p => p.PictureName)
            .HasMaxLength(500)
            .IsRequired()
            .HasComment("آدرس عکس");

        builder.Property(p => p.PictureAlt)
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("آلت عکس برای سئو");

        builder.Property(p => p.PictureTitle)
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("عنوان عکس برای سئو");

        builder.Property(p => p.DisplayOrder)
            .IsRequired()
            .HasComment("اولویت نمایش");

        builder.Property(p => p.PictureType)
            .HasDefaultValue(PictureType.ProductCategory)
            .IsRequired()
            .HasComment("نوع عکس");
    }
}
