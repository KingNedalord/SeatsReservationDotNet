using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.SessionSeatsDto;

namespace SeatsReservationDotNet.Services.Interfaces;

public interface ISessionSeatService
{
    Task<GetSessionSeatDto> CreateSessionSeatAsync(SaveSessionSeatDto dto);
    Task<PagedResult<GetSessionSeatDto>> GetAllSessionSeatsAsync(int page, int size);
    Task<GetSessionSeatDto> GetSessionSeatAsync(long id);
    Task<GetSessionSeatDto> UpdateSessionSeatAsync(long id, SaveSessionSeatDto dto);
    Task DeleteSessionSeatAsync(long id);
}