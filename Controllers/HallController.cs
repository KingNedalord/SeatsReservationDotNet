using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.HallDto;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("halls")]
[Authorize]
public class HallController(IHallService service) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetHallDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHall([FromBody] SaveHallDto dto)
    {
        var result = await service.CreateHallAsync(dto);
        return CreatedAtAction(nameof(GetHall), new { id = result.Id }, result);
    }
    
    [HttpGet]
    [ProducesResponseType<PagedResult<GetHallDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetHallDto>> GetAllHalls(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await service.GetAllHallsAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetHallDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetHallDto> GetHall(long id)
        => await service.GetHallAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetHallDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetHallDto> UpdateHall(long id, [FromBody] SaveHallDto dto)
        => await service.UpdateHallAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteHall(long id)
    {
        await service.DeleteHallAsync(id);
        return NoContent();
    }
}