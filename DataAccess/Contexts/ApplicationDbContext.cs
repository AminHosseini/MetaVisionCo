using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Context.Extensions;
using Domain.Models;

namespace DataAccess.Contexts;

/// <summary>
/// کلاس زمینه پایگاه داده برنامه
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// سازنده کلاس
    /// </summary>
    public ApplicationDbContext(DbContextOptions options) : base()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Shop");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyAll();
    }

    /// <summary>
    /// دسته بندی های محصول
    /// </summary>
    public required DbSet<ProductCategory> ProductCategories { get; set; } = default!;
}
