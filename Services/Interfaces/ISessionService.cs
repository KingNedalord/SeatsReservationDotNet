using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.Session;

namespace SeatsReservationDotNet.Services.Interfaces;

public interface ISessionService
{
    Task<GetSessionDto> CreateSessionAsync(SaveSessionDto dto);
    Task<PagedResult<GetSessionDto>> GetAllSessionsAsync(int page, int size);
    Task<GetSessionDto> GetSessionAsync(long id);
    Task<GetSessionDto> UpdateSessionAsync(long id, SaveSessionDto dto);
    Task DeleteSessionAsync(long id);
}
