using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.HallDto;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class HallService(AppDbContext context, IMapper mapper) : IHallService
{
    public async Task<GetHallDto> CreateHallAsync(SaveHallDto dto)
    {
        var entity = mapper.Map<Hall>(dto);
        context.Halls.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<GetHallDto>(entity);
    }

    public async Task<PagedResult<GetHallDto>> GetAllHallsAsync(int page, int size)
    {
        var query = context.Halls.AsQueryable();
        var total = await query.CountAsync();
        var entities = await query
            .AsNoTracking()
            .Skip(page * size)
            .Take(size)
            .Select(h => mapper.Map<GetHallDto>(h))
            .ToListAsync();

        return new PagedResult<GetHallDto>(entities, total, page, size);
    }

    public async Task<GetHallDto> GetHallAsync(long id)
    {
        var seat = await context.Halls
                       .AsNoTracking()
                       .FirstOrDefaultAsync(h => h.Id == id)
                   ?? throw new KeyNotFoundException($"Hall with id {id} not found");
        return mapper.Map<GetHallDto>(seat);
    }

    public async Task<GetHallDto> UpdateHallAsync(long id, SaveHallDto dto)
    {
        var entity = await context.Halls.FindAsync(id)
                   ?? throw new KeyNotFoundException($"Hall with id {id} not found");
        
        entity.CinemaId = dto.CinemaId;
        entity.Name = dto.Name;

        await context.SaveChangesAsync();
        return mapper.Map<GetHallDto>(entity);
    }

    public async Task DeleteHallAsync(long id)
    {
        var rowsAffected = await context.Halls
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Hall with id {id} not found");
        }
    }
}