using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("seats")]
public class Seat
{
    [Column("id")]
    public long Id { get; set; }

    [Column("hall_id")]
    public long HallId { get; set; }

    [Column("price_category_id")]
    public long PriceCategoryId { get; set; }

    [Column("row")]
    public int? Row { get; set; }

    [Column("number")]
    public int? Number { get; set; }

    [Column("status")]
    public SeatStatus? Status { get; set; }

    [Column("is_available")]
    public bool? IsAvailable { get; set; }

    [Column("comment")]
    [MaxLength(500)]
    public string? Comment { get; set; }

    public Hall Hall { get; set; } = null!;
    public PriceCategory PriceCategory { get; set; } = null!;
}
