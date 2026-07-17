namespace SeatsReservationDotNet.DTOs.HallDto;

public class GetHallDto
{
    public long Id { get; set; }

    public long CinemaId { get; set; }

    public string? Name { get; set; }
}