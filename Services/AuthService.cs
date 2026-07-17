using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs.RegistrationDtos;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class AuthService(AppDbContext db, PasswordHasher hasher, JwtTokenGenerator jwt): IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await db.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email))
        {
            throw new InvalidOperationException("Username or Email already exists");
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hasher.Hash(request.Password),
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var token = jwt.GenerateToken(user);
        return new AuthResponse(token, user.Username, user.Role);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await db.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user is null || !hasher.Verify(request.Password, user.PasswordHash))
        {
            throw new InvalidOperationException("Invalid username or password");
        }

        var token = jwt.GenerateToken(user);
        return new AuthResponse(token, user.Username, user.Role);
    }
}