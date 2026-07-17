using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("sessions")]
public class Session
{
    [Column("id")]
    public long Id { get; set; }

    [Column("movie_id")]
    public long MovieId { get; set; }

    [Column("hall_id")]
    public long HallId { get; set; }

    [Column("title")]
    [MaxLength(500)]
    public string? Title { get; set; }

    [Column("date")]
    public DateOnly? Date { get; set; }

    [Column("time")]
    public TimeOnly? Time { get; set; }

    [Column("language")]
    public MovieLang? Language { get; set; }

    [Column("format")]
    [MaxLength(50)]
    public MovieFormat? Format { get; set; }

    public Movie Movie { get; set; } = null!;
    public Hall Hall { get; set; } = null!;
}
