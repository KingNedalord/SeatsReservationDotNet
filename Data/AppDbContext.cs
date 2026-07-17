using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Entities;

namespace SeatsReservationDotNet.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<PriceCategory> PriceCategories { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionSeat> SessionSeats { get; set; }
    
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.Genre });

        // Store all enums as strings to match the PostgreSQL VARCHAR columns
        modelBuilder.Entity<Movie>()
            .Property(m => m.AgeRating)
            .HasConversion<string>();

        modelBuilder.Entity<PriceCategory>()
            .Property(pc => pc.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Seat>()
            .Property(s => s.Status)
            .HasConversion<string>();

        modelBuilder.Entity<MovieGenre>()
            .Property(mg => mg.Genre)
            .HasConversion<string>();

        modelBuilder.Entity<Session>()
            .Property(s => s.Language)
            .HasConversion<string>();

        modelBuilder.Entity<Session>()
            .Property(s => s.Format)
            .HasConversion<string>();

        // Range of decimal variables in db
        modelBuilder.Entity<PriceCategory>()
            .Property(pc => pc.Price)
            .HasColumnType("numeric(10,2)");

        modelBuilder.Entity<Movie>()
            .Property(mv => mv.Rating)
            .HasColumnType("numeric(10,2)");
        
        // User specifics
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        // Indexes matching the SQL schema
        modelBuilder.Entity<Hall>()
            .HasIndex(h => h.CinemaId).HasDatabaseName("idx_halls_cinema_id");
        modelBuilder.Entity<MovieGenre>()
            .HasIndex(mg => mg.MovieId).HasDatabaseName("idx_movie_genres_movie_id");
        modelBuilder.Entity<Seat>()
            .HasIndex(s => s.HallId).HasDatabaseName("idx_seats_hall_id");
        modelBuilder.Entity<Seat>()
            .HasIndex(s => s.PriceCategoryId).HasDatabaseName("idx_seats_price_category_id");
        modelBuilder.Entity<Session>()
            .HasIndex(s => s.MovieId).HasDatabaseName("idx_sessions_movie_id");
        modelBuilder.Entity<Session>()
            .HasIndex(s => s.HallId).HasDatabaseName("idx_sessions_hall_id");
        modelBuilder.Entity<SessionSeat>()
            .HasIndex(ss => ss.SessionId).HasDatabaseName("idx_session_seats_session_id");
        modelBuilder.Entity<SessionSeat>()
            .HasIndex(ss => new { ss.SessionId, ss.SeatId })
            .IsUnique();
        modelBuilder.Entity<SessionSeat>()
            .HasIndex(ss => ss.SeatId).HasDatabaseName("idx_session_seats_seat_id");
    }
}
