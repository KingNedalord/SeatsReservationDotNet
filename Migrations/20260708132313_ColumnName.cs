using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatsReservationDotNet.Migrations
{
    /// <inheritdoc />
    public partial class ColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "base_schema",
                table: "users",
                newName: "role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                schema: "base_schema",
                table: "users",
                newName: "Role");
        }
    }
}
