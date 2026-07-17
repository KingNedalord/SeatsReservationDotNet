using SeatsReservationDotNet.DTOs.RegistrationDtos;

namespace SeatsReservationDotNet.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}