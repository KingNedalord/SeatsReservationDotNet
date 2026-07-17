using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.SessionSeatsDto;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("session-seats")]
[Authorize]
public class SessionSeatsController(ISessionSeatService service) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetSessionSeatDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSessionSeat([FromBody] SaveSessionSeatDto dto)
    {
        var result = await service.CreateSessionSeatAsync(dto);
        return CreatedAtAction(nameof(GetSessionSeat), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetSessionSeatDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetSessionSeatDto>> GetAllSessionSeats(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await service.GetAllSessionSeatsAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSessionSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetSessionSeatDto> GetSessionSeat(long id)
        => await service.GetSessionSeatAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSessionSeatDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSessionSeatDto> UpdateSessionSeat(long id, [FromBody] SaveSessionSeatDto dto)
        => await service.UpdateSessionSeatAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSessionSeat(long id)
    {
        await service.DeleteSessionSeatAsync(id);
        return NoContent();
    }
}