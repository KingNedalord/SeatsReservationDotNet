using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.HallDto;

namespace SeatsReservationDotNet.Services.Interfaces;

public interface IHallService
{
    Task<GetHallDto> CreateHallAsync(SaveHallDto dto);
    Task<PagedResult<GetHallDto>> GetAllHallsAsync(int page, int size);
    Task<GetHallDto> GetHallAsync(long id);
    Task<GetHallDto> UpdateHallAsync(long id, SaveHallDto dto);
    Task DeleteHallAsync(long id);
}