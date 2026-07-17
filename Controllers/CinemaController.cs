using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.CinemaDto;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("cinemas")]
[Authorize]
public class CinemaController(ICinemaService service): ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetCinemaDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCinema([FromBody] SaveCinemaDto cinema)
    {
        var result = await service.CreateCinemaAsync(cinema);
        return CreatedAtAction(nameof(GetCinema), new  { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetCinemaDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetCinemaDto>> GetAllCinemas(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        =>  await service.GetAllCinemasAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetCinemaDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetCinemaDto> GetCinema(long id) =>  await service.GetCinemaAsync(id);
    
    [HttpPut("{id:long}")]
    [ProducesResponseType<GetCinemaDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetCinemaDto> UpdateCinema(
        long id,
        [FromBody] SaveCinemaDto cinema) 
        => await service.UpdateCinemaAsync(id, cinema);
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCinema(long id)
    {
        await service.DeleteCinemaAsync(id);
        return NoContent();
    }
}