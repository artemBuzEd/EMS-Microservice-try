using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class v_11AddedIndexInUserCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserComment_event_id",
                table: "UserComment",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserComment_user_id",
                table: "UserComment",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserComment_event_id",
                table: "UserComment");

            migrationBuilder.DropIndex(
                name: "IX_UserComment_user_id",
                table: "UserComment");
        }
    }
}
