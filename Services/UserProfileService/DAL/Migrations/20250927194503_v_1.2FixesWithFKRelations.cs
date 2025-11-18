using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class v_12FixesWithFKRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComment_UserProfile_UserProfileuser_id",
                table: "UserComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEventCalendar_UserProfile_UserProfileuser_id",
                table: "UserEventCalendar");

            migrationBuilder.DropIndex(
                name: "IX_UserEventCalendar_UserProfileuser_id",
                table: "UserEventCalendar");

            migrationBuilder.DropIndex(
                name: "IX_UserComment_UserProfileuser_id",
                table: "UserComment");

            migrationBuilder.DropColumn(
                name: "UserProfileuser_id",
                table: "UserEventCalendar");

            migrationBuilder.DropColumn(
                name: "UserProfileuser_id",
                table: "UserComment");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserEventCalendar",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComment_UserProfile_user_id",
                table: "UserComment",
                column: "user_id",
                principalTable: "UserProfile",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEventCalendar_UserProfile_user_id",
                table: "UserEventCalendar",
                column: "user_id",
                principalTable: "UserProfile",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComment_UserProfile_user_id",
                table: "UserComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEventCalendar_UserProfile_user_id",
                table: "UserEventCalendar");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserEventCalendar",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileuser_id",
                table: "UserEventCalendar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserProfileuser_id",
                table: "UserComment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEventCalendar_UserProfileuser_id",
                table: "UserEventCalendar",
                column: "UserProfileuser_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserComment_UserProfileuser_id",
                table: "UserComment",
                column: "UserProfileuser_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComment_UserProfile_UserProfileuser_id",
                table: "UserComment",
                column: "UserProfileuser_id",
                principalTable: "UserProfile",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEventCalendar_UserProfile_UserProfileuser_id",
                table: "UserEventCalendar",
                column: "UserProfileuser_id",
                principalTable: "UserProfile",
                principalColumn: "user_id");
        }
    }
}
