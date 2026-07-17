using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeatsReservationDotNet.Entities;

[Table("users")]
public class User
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("username")]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Column("email")]
    [MaxLength(70)]
    public string Email { get; set; } = string.Empty;

    [Column("passwordHash")]
    [MaxLength(100)]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("role")]
    public UserRole Role { get; set; } = UserRole.User;
}

public enum UserRole
{
    User = 0,
    Admin = 1,
}