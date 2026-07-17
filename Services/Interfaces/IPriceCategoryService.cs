using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.PriceCategoryDto;

namespace SeatsReservationDotNet.Services.Interfaces;

public interface IPriceCategoryService
{
    Task<GetPriceCategoryDto> CreatePriceCategoryAsync(SavePriceCategoryDto dto);
    Task<PagedResult<GetPriceCategoryDto>> GetAllPriceCategoriesAsync(int page, int size);
    Task<GetPriceCategoryDto> GetPriceCategoryAsync(long id);
    Task<GetPriceCategoryDto> UpdatePriceCategoryAsync(long id, SavePriceCategoryDto dto);
    Task DeletePriceCategoryAsync(long id);
}