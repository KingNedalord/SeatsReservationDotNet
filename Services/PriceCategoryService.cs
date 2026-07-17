using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.PriceCategoryDto;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class PriceCategoryService(AppDbContext context, IMapper mapper) : IPriceCategoryService
{
    public async Task<GetPriceCategoryDto> CreatePriceCategoryAsync(SavePriceCategoryDto dto)
    {
        var entity = mapper.Map<PriceCategory>(dto);
        context.PriceCategories.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<GetPriceCategoryDto>(entity);
    }

    public async Task<PagedResult<GetPriceCategoryDto>> GetAllPriceCategoriesAsync(int page, int size)
    {
        var query = context.PriceCategories.AsQueryable();
        var total = await query.CountAsync();
        var entities = await query
            .AsNoTracking()
            .Skip(page * size)
            .Take(size)
            .Select(pc => mapper.Map<GetPriceCategoryDto>(pc))
            .ToListAsync();

        return new PagedResult<GetPriceCategoryDto>(entities, total, page, size);
    }

    public async Task<GetPriceCategoryDto> GetPriceCategoryAsync(long id)
    {
        var entity = await context.PriceCategories
                         .AsNoTracking()
                         .FirstOrDefaultAsync(pc => pc.Id == id)
                   ?? throw new KeyNotFoundException($"Price Category with id {id} not found");
        return mapper.Map<GetPriceCategoryDto>(entity);
    }

    public async Task<GetPriceCategoryDto> UpdatePriceCategoryAsync(long id, SavePriceCategoryDto dto)
    {
        var entity = await context.PriceCategories.FindAsync(id)
                   ?? throw new KeyNotFoundException($"Price Category with id {id} not found");
        
        entity.Name = dto.Name;
        entity.Price = dto.Price;
        entity.Type = dto.Type;

        await context.SaveChangesAsync();
        return mapper.Map<GetPriceCategoryDto>(entity);
    }

    public async Task DeletePriceCategoryAsync(long id)
    {
        var rowsAffected = await context.PriceCategories
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Price Category with id {id} not found");
        }
    }
}