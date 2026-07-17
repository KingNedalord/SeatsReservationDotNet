using System.ComponentModel.DataAnnotations;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs.PriceCategoryDto;

public class SavePriceCategoryDto
{
    [Required]
    public PriceCategoryEnum Type { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public decimal? Price { get; set; }
}