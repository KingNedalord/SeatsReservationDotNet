using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.CinemaDto;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class CinemaService(AppDbContext context, IMapper mapper) : ICinemaService
{
    public async Task<GetCinemaDto> CreateCinemaAsync(SaveCinemaDto dto)
    {
        var entity = mapper.Map<Cinema>(dto);
        context.Cinemas.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<GetCinemaDto>(entity);
    }

    public async Task<PagedResult<GetCinemaDto>> GetAllCinemasAsync(int page, int size)
    {
        var query = context.Cinemas.AsQueryable();
        var total = await query.CountAsync();
        var entities = await query
            .AsNoTracking()
            .Skip(page * size)
            .Take(size)
            .Select(entity => mapper.Map<GetCinemaDto>(entity))
            .ToListAsync();

        return new PagedResult<GetCinemaDto>(entities, total, page, size);
    }

    public async Task<GetCinemaDto> GetCinemaAsync(long id)
    {
        var entity = await context.Cinemas
                         .AsNoTracking()
                         .FirstOrDefaultAsync(c => c.Id == id) 
                     ??  throw new KeyNotFoundException($"Cinema with id {id} not found");
        return mapper.Map<GetCinemaDto>(entity);
    }

    public async Task<GetCinemaDto> UpdateCinemaAsync(long id, SaveCinemaDto dto)
    {
        var entity = await context.Cinemas.FindAsync(id)
                     ?? throw new KeyNotFoundException($"Cinema with id {id} not found");

        entity.Name = dto.Name;
        entity.Address = dto.Address;
        entity.City = dto.City; 
        await context.SaveChangesAsync(); 

        return mapper.Map<GetCinemaDto>(entity);
    }

    public async Task DeleteCinemaAsync(long id)
    {
        var rowsAffected = await context.Cinemas
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Cinema with id {id} not found");
        }
    }
}