using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.PriceCategoryDto;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Controllers;

[ApiController]
[Route("price-categories")]
[Authorize]
public class PriceCategoryController(IPriceCategoryService service) : ControllerBase
{
    private const int DefaultPageSize = 20;

    [HttpPost]
    [ProducesResponseType<GetPriceCategoryDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePriceCategory([FromBody] SavePriceCategoryDto dto)
    {
        var result = await service.CreatePriceCategoryAsync(dto);
        return CreatedAtAction(nameof(GetPriceCategory), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<GetPriceCategoryDto>>(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<PagedResult<GetPriceCategoryDto>> GetAllPriceCategories(
        [FromQuery] int page = 0,
        [FromQuery] int size = DefaultPageSize)
        => await service.GetAllPriceCategoriesAsync(page, size);

    [HttpGet("{id:long}")]
    [ProducesResponseType<GetPriceCategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<GetPriceCategoryDto> GetPriceCategory(long id)
        => await service.GetPriceCategoryAsync(id);

    [HttpPut("{id:long}")]
    [ProducesResponseType<GetPriceCategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetPriceCategoryDto> UpdatePriceCategory(long id, [FromBody] SavePriceCategoryDto dto)
        => await service.UpdatePriceCategoryAsync(id, dto);

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePriceCategory(long id)
    {
        await service.DeletePriceCategoryAsync(id);
        return NoContent();
    }
}