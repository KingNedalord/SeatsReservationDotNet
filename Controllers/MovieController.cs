using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.MovieDto;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("movies")]
[Authorize]
public class MovieController(IMovieService service) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetMovieDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie([FromBody] SaveMovieDto dto)
    {
        var result = await service.CreateMovieAsync(dto);
        return CreatedAtAction(nameof(GetMovie), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetMovieDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetMovieDto>> GetAllMovies(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await service.GetAllMoviesAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetMovieDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetMovieDto> GetMovie(long id)
        => await service.GetMovieAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetMovieDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetMovieDto> UpdateMovie(long id, [FromBody] SaveMovieDto dto)
        => await service.UpdateMovieAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMovie(long id)
    {
        await service.DeleteMovieAsync(id);
        return NoContent();
    }
}