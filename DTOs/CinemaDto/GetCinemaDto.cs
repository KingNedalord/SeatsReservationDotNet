namespace SeatsReservationDotNet.DTOs.CinemaDto;

public class GetCinemaDto
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }
}