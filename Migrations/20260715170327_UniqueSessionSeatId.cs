using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatsReservationDotNet.Migrations
{
    /// <inheritdoc />
    public partial class UniqueSessionSeatId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_session_seats_session_id_seat_id",
                schema: "base_schema",
                table: "session_seats",
                columns: new[] { "session_id", "seat_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_session_seats_session_id_seat_id",
                schema: "base_schema",
                table: "session_seats");
        }
    }
}
