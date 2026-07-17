using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.DTOs.PriceCategoryDto;

public class GetPriceCategoryDto
{
    public long Id { get; set; }
    public PriceCategoryEnum? Type { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
}