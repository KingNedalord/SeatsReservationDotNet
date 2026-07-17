using System.ComponentModel.DataAnnotations.Schema;
using SeatsReservationDotNet.Enums;

namespace SeatsReservationDotNet.Entities;

[Table("movie_genres")]
public class MovieGenre
{
    [Column("movie_id")]
    public long MovieId { get; set; }

    [Column("genre")]
    public Genre Genre { get; set; }

    public Movie Movie { get; set; } = null!;
}
