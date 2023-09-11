using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeeSharp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "BlogPosts");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "BlogPosts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_AspNetUsers_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_AspNetUsers_AuthorId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "BlogPosts");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
