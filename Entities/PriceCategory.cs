using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("price_category")]
public class PriceCategory
{
    [Column("id")]
    public long Id { get; set; }

    [Column("type")]
    public PriceCategoryEnum? Type { get; set; }

    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    [Column("price")]
    public decimal? Price { get; set; }
}
