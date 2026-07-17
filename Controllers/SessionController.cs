using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.Session;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("sessions")]
[Authorize]
public class SessionController(ISessionService service) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSession([FromBody] SaveSessionDto dto)
    {
        var result = await service.CreateSessionAsync(dto);
        return CreatedAtAction(nameof(GetSession), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetSessionDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetSessionDto>> GetAllSessions(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await service.GetAllSessionsAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetSessionDto> GetSession(long id)
        => await service.GetSessionAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetSessionDto> UpdateSession(long id, [FromBody] SaveSessionDto dto)
        => await service.UpdateSessionAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSession(long id)
    {
        await service.DeleteSessionAsync(id);
        return NoContent();
    }
}
