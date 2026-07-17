using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatsReservationDotNet.Migrations
{
    /// <inheritdoc />
    public partial class SessionSeatPropsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_available",
                schema: "base_schema",
                table: "session_seats");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "base_schema",
                table: "session_seats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_available",
                schema: "base_schema",
                table: "session_seats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "status",
                schema: "base_schema",
                table: "session_seats",
                type: "text",
                nullable: true);
        }
    }
}
