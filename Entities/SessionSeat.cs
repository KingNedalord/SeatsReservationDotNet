using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("session_seats")]
public class SessionSeat
{
    [Column("id")]
    public long Id { get; set; }

    [Column("session_id")]
    public long SessionId { get; set; }

    [Column("seat_id")]
    public long SeatId { get; set; }

    [Column("customer_name")]
    [MaxLength(255)]
    public string? CustomerName { get; set; }

    [Column("contact")]
    [MaxLength(255)]
    public string? Contact { get; set; }

    public Session Session { get; set; } = null!;
    public Seat Seat { get; set; } = null!;
}
