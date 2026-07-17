namespace SeatsReservationDotNet.DTOs.SessionSeatsDto;

public class GetSessionSeatDto
{
    public long Id { get; set; }
    public long SessionId { get; set; }
    public long SeatId { get; set; }
    public string? CustomerName { get; set; }
    public string? Contact { get; set; }
}
