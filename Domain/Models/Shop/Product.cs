namespace Domain.Models.Shop;

/// <summary>
/// محصول
/// </summary>
public class Product : BaseEntity, IHaveSoftDelete, IHaveCreationInfo
{
    /// <summary>
    /// آیدی دسته بندی محصول
    /// </summary>
    public required long ProductCategoryId { get; set; }

    /// <summary>
    /// دسته بندی محصول
    /// </summary>
    public required ProductCategory ProductCategory { get; set; }

    /// <summary>
    /// عنوان
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// کد محصول
    /// </summary>
    //public required string Code { get; set; }

    /// <summary>
    /// توضیحات کوتاه
    /// </summary>
    public required string ShortDescription { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// ‍پراپرتی های سئو
    /// </summary>
    public Seo Seo { get; set; }

    [SetsRequiredMembers]
    public Product()
    {
        Name = string.Empty;
        //Code = string.Empty;
        ShortDescription = string.Empty;
        Description = string.Empty;
        Seo = new Seo();
        ProductCategory = default!;
    }

    [SetsRequiredMembers]
    public Product(long id)
    {
        Id = id;
        Name = string.Empty;
        //Code = string.Empty;
        ShortDescription = string.Empty;
        Description = string.Empty;
        Seo = new Seo();
        ProductCategory = default!;
    }
}
