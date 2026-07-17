using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatsReservationDotNet.Migrations
{
    /// <inheritdoc />
    public partial class BaseSchemaDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "users",
                schema: "base_schema",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "sessions",
                schema: "base_schema",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "session_seats",
                schema: "base_schema",
                newName: "session_seats");

            migrationBuilder.RenameTable(
                name: "seats",
                schema: "base_schema",
                newName: "seats");

            migrationBuilder.RenameTable(
                name: "price_category",
                schema: "base_schema",
                newName: "price_category");

            migrationBuilder.RenameTable(
                name: "movies",
                schema: "base_schema",
                newName: "movies");

            migrationBuilder.RenameTable(
                name: "movie_genres",
                schema: "base_schema",
                newName: "movie_genres");

            migrationBuilder.RenameTable(
                name: "halls",
                schema: "base_schema",
                newName: "halls");

            migrationBuilder.RenameTable(
                name: "cinemas",
                schema: "base_schema",
                newName: "cinemas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base_schema");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "users",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "sessions",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "session_seats",
                newName: "session_seats",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "seats",
                newName: "seats",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "price_category",
                newName: "price_category",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "movies",
                newName: "movies",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "movie_genres",
                newName: "movie_genres",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "halls",
                newName: "halls",
                newSchema: "base_schema");

            migrationBuilder.RenameTable(
                name: "cinemas",
                newName: "cinemas",
                newSchema: "base_schema");
        }
    }
}
