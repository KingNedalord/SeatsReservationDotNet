using System.ComponentModel.DataAnnotations;

namespace SeatsReservationDotNet.DTOs.CinemaDto;

public class SaveCinemaDto
{
    [Required] public string? Name { get; set; }
    
    [Required]public string? Address { get; set; }

    [Required] public string? City { get; set; }
}