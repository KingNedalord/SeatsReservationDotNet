using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.MovieDto;

namespace SeatsReservationDotNet.Services.Interfaces;

public interface IMovieService
{
    Task<GetMovieDto> CreateMovieAsync(SaveMovieDto dto);
    Task<PagedResult<GetMovieDto>> GetAllMoviesAsync(int page, int size);
    Task<GetMovieDto> GetMovieAsync(long id);
    Task<GetMovieDto> UpdateMovieAsync(long id, SaveMovieDto dto);
    Task DeleteMovieAsync(long id);
}