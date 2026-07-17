using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatsReservationDotNet.Entities;

[Table("halls")]
public class Hall
{
    [Column("id")]
    public long Id { get; set; }

    [Column("cinema_id")]
    public long CinemaId { get; set; }

    [Column("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    public Cinema Cinema { get; set; } = null!;
    public ICollection<Seat> Seats { get; set; } = [];
    public ICollection<Session> Sessions { get; set; } = [];
}
