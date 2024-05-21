using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductCategorycreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Shop");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                schema: "Shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true, comment: "آیدی دسته بندی اصلی محصول"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "عنوان"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "توضیحات"),
                    Seo_Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "اسلاگ برای سئو"),
                    Seo_Keywords = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "کلمات کلیدی برای سئو"),
                    Seo_MetaDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "توضیحات متا برای سئو"),
                    _CreatedByUser = table.Column<long>(type: "bigint", nullable: false, comment: "کاربر سازنده"),
                    _CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, comment: "تاریخ ساخت"),
                    _DeleteDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, comment: "تاریخ حذف"),
                    _DeletedByUser = table.Column<long>(type: "bigint", nullable: true, comment: "کاربر حذف کننده"),
                    _IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    _RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "بررسی همزمانی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                },
                comment: "دسته بندی های محصول");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "Shop");
        }
    }
}
