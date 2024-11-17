using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Productcreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false, comment: "آیدی دسته بندی محصول"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "عنوان"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "کد محصولی"),
                    ShortDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "توضیحات کوتاه"),
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
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalSchema: "Shop",
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "محصول ها");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                schema: "Shop",
                table: "Products",
                column: "ProductCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "Shop");
        }
    }
}
