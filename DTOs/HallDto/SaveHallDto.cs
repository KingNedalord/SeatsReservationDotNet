using System.ComponentModel.DataAnnotations;

namespace SeatsReservationDotNet.DTOs.HallDto;

public class SaveHallDto
{
    [Required]
    public long CinemaId { get; set; }
    
    [Required]
    public string? Name { get; set; }
}