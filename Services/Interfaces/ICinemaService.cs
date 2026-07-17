using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.CinemaDto;

namespace SeatsReservationDotNet.Services.Interfaces;

public interface ICinemaService
{
    Task<GetCinemaDto> CreateCinemaAsync(SaveCinemaDto dto);
    Task<PagedResult<GetCinemaDto>> GetAllCinemasAsync(int page, int size);
    Task<GetCinemaDto> GetCinemaAsync(long id);
    Task<GetCinemaDto> UpdateCinemaAsync(long id, SaveCinemaDto dto);
    Task DeleteCinemaAsync(long id);
}