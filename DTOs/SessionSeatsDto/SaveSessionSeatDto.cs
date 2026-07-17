using System.ComponentModel.DataAnnotations;

namespace SeatsReservationDotNet.DTOs.SessionSeatsDto;

public class SaveSessionSeatDto
{
    [Required]
    public long SessionId { get; set; }

    [Required]
    public long SeatId { get; set; }

    [Required]
    public string? CustomerName { get; set; }

    [Required]
    public string? Contact { get; set; }
}