using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Picturecreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Main");

            migrationBuilder.CreateTable(
                name: "Pictures",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: "آیدی موجودیت مادر"),
                    PictureName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "آدرس عکس"),
                    PictureAlt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "آلت عکس برای سئو"),
                    PictureTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "عنوان عکس برای سئو"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, comment: "اولویت نمایش"),
                    PictureType = table.Column<int>(type: "int", nullable: false, defaultValue: 1, comment: "نوع عکس"),
                    _CreatedByUser = table.Column<long>(type: "bigint", nullable: false, comment: "کاربر سازنده"),
                    _CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, comment: "تاریخ ساخت"),
                    _DeleteDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, comment: "تاریخ حذف"),
                    _DeletedByUser = table.Column<long>(type: "bigint", nullable: true, comment: "کاربر حذف کننده"),
                    _IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    _RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "بررسی همزمانی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                },
                comment: "عکس ها");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures",
                schema: "Main");
        }
    }
}
