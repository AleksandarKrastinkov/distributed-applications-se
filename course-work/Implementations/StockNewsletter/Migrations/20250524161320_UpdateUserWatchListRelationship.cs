using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockNewsletter.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserWatchListRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLists_Users_UserId",
                table: "WatchLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchLists_Users_UserId",
                table: "WatchLists");

            migrationBuilder.DropIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists");
        }
    }
}
