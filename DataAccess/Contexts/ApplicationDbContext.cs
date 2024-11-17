using System.Reflection;
using BaseLib.Context.Extensions;

namespace DataAccess.Contexts;

/// <summary>
/// کلاس زمینه پایگاه داده برنامه
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    ///// <summary>
    ///// سازنده کلاس
    ///// </summary>
    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //{
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("Shop");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyAll();
    }

    #region Shop

    /// <summary>
    /// دسته بندی های محصول
    /// </summary>
    public required DbSet<ProductCategory> ProductCategories { get; set; } = default!;

    /// <summary>
    /// محصول ها
    /// </summary>
    public required DbSet<Product> Products { get; set; } = default!;

    #endregion

    #region Others

    /// <summary>
    /// عکس ها
    /// </summary>
    public required DbSet<Picture> Pictures { get; set; } = default!;

    #endregion
}
