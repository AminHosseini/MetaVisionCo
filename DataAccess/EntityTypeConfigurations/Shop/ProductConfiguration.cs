namespace DataAccess.EntityTypeConfigurations.Shop;

/// <summary>
/// کلاس پیکربندی موجودیت های محصول
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <summary>
    /// تنظیمات موجودیت های محصول
    /// </summary>
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder = builder.ToTable("Products", "Shop",
            t => t.HasComment("محصول ها")
        );

        builder.Property(p => p.ProductCategoryId)
            .HasComment("آیدی دسته بندی محصول");

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired()
            .HasComment("عنوان");

        builder.Property(p => p.Code)
            .HasMaxLength(50)
            .IsRequired()
            .HasComment("کد محصولی");

        builder.Property(p => p.ShortDescription)
            .HasMaxLength(300)
            .IsRequired()
            .HasComment("توضیحات کوتاه");

        builder.Property(p => p.Description)
            .HasMaxLength(1000)
            .IsRequired()
            .HasComment("توضیحات");

        builder = builder.OwnsOne(p => p.Seo, nb =>
        {
            nb.Property(s => s.Slug)
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("اسلاگ برای سئو");

            nb.Property(s => s.Keywords)
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("کلمات کلیدی برای سئو");

            nb.Property(s => s.MetaDescription)
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("توضیحات متا برای سئو");
        });
    }
}
