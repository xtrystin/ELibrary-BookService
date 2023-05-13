using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELibrary_BookService.Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixedTageTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Tag_TageId",
                schema: "bookService",
                table: "BookTag");

            migrationBuilder.RenameColumn(
                name: "TageId",
                schema: "bookService",
                table: "BookTag",
                newName: "TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_BookTag_TageId",
                schema: "bookService",
                table: "BookTag",
                newName: "IX_BookTag_TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Tag_TagsId",
                schema: "bookService",
                table: "BookTag",
                column: "TagsId",
                principalSchema: "bookService",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Tag_TagsId",
                schema: "bookService",
                table: "BookTag");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                schema: "bookService",
                table: "BookTag",
                newName: "TageId");

            migrationBuilder.RenameIndex(
                name: "IX_BookTag_TagsId",
                schema: "bookService",
                table: "BookTag",
                newName: "IX_BookTag_TageId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Tag_TageId",
                schema: "bookService",
                table: "BookTag",
                column: "TageId",
                principalSchema: "bookService",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
