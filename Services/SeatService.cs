using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.SeatDto;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class SeatService(AppDbContext context, IMapper mapper) : ISeatService
{
    public async Task<GetSeatDto> CreatePlaceAsync(SaveSeatDto dto)
    {
        var entity = mapper.Map<Seat>(dto);
        context.Seats.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<GetSeatDto>(entity);
    }

    public async Task<PagedResult<GetSeatDto>> GetAllPlacesAsync(int page, int size)
    {
        var query = context.Seats.AsNoTracking();
        var total = await query.CountAsync();
        var entities = await query
            .OrderBy(s => s.Row).ThenBy(s => s.Number)
            .Skip(page * size)
            .Take(size)
            .Select(s => mapper.Map<GetSeatDto>(s))
            .ToListAsync();

        return new PagedResult<GetSeatDto>(entities, total, page, size);
    }

    public async Task<GetSeatDto> GetPlaceAsync(long id)
    {
        var entity = await context.Seats.FindAsync(id)
            ?? throw new KeyNotFoundException($"Seat with id {id} not found");
        return mapper.Map<GetSeatDto>(entity);
    }

    public async Task<GetSeatDto> UpdatePlaceAsync(long id, SaveSeatDto dto)
    {
        var entity = await context.Seats.FindAsync(id)
            ?? throw new KeyNotFoundException($"Seat with id {id} not found");

        entity.Row = dto.Row;
        entity.Number = dto.Number;
        entity.Status = dto.Status;
        entity.IsAvailable = dto.IsAvailable;
        entity.Comment = dto.Comment;

        await context.SaveChangesAsync();
        return mapper.Map<GetSeatDto>(entity);
    }

    public async Task DeletePlaceAsync(long id)
    {
        var entity = await context.Seats.FindAsync(id)
            ?? throw new KeyNotFoundException($"Seat with id {id} not found");
        context.Seats.Remove(entity);
        await context.SaveChangesAsync();
    }
}
