using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.SeatDto;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("seats")]
[Authorize]
public class SeatController(ISeatService service) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePlace([FromBody] SaveSeatDto dto)
    {
        var result = await service.CreatePlaceAsync(dto);
        return CreatedAtAction(nameof(GetPlace), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetSeatDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetSeatDto>> GetAllPlaces(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await service.GetAllPlacesAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetSeatDto> GetPlace(long id)
        => await service.GetPlaceAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSeatDto> UpdatePlace(long id, [FromBody] SaveSeatDto dto)
        => await service.UpdatePlaceAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePlace(long id)
    {
        await service.DeletePlaceAsync(id);
        return NoContent();
    }
}
