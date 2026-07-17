using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("movies")]
public class Movie
{
    [Column("id")]
    public long Id { get; set; }

    [Column("title")]
    [MaxLength(500)]
    public string? Title { get; set; }

    [Column("duration_minutes")]
    public int? DurationMinutes { get; set; }

    [Column("age_rating")]
    public AgeRating? AgeRating { get; set; }

    [Column("rating")]
    public decimal? Rating { get; set; }

    [Column("poster_url")]
    [MaxLength(1000)]
    public string? PosterUrl { get; set; }

    [Column("description")]
    [MaxLength(10000)]
    public string? Description { get; set; }

    [Column("release_year")]
    public int? ReleaseYear { get; set; }

    public ICollection<MovieGenre> Genres { get; set; } = [];
}
