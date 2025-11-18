using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class v_10InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    bio = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateTime>(type: "DATE", nullable: false),
                    created_at = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "CURRENT_DATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "UserComment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    added_at = table.Column<DateTime>(type: "DATE", nullable: false),
                    is_changed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UserProfileuser_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComment", x => x.id);
                    table.CheckConstraint("CK_USER_RATING", "rating >= 0 AND rating <= 5");
                    table.ForeignKey(
                        name: "FK_UserComment_UserProfile_UserProfileuser_id",
                        column: x => x.UserProfileuser_id,
                        principalTable: "UserProfile",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "UserEventCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    registration_id = table.Column<int>(type: "integer", nullable: true),
                    added_at = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserProfileuser_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEventCalendar", x => x.Id);
                    table.CheckConstraint("CK_STATUS_OF_EVENT", "status IN ('Registered','Interested','Attended')");
                    table.ForeignKey(
                        name: "FK_UserEventCalendar_UserProfile_UserProfileuser_id",
                        column: x => x.UserProfileuser_id,
                        principalTable: "UserProfile",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserComment_UserProfileuser_id",
                table: "UserComment",
                column: "UserProfileuser_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserEventCalendar_user_id",
                table: "UserEventCalendar",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserEventCalendar_UserProfileuser_id",
                table: "UserEventCalendar",
                column: "UserProfileuser_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_user_id",
                table: "UserProfile",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserComment");

            migrationBuilder.DropTable(
                name: "UserEventCalendar");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
