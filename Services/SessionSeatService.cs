using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.SessionSeatsDto;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class SessionSeatService(AppDbContext context, IMapper mapper) : ISessionSeatService
{
    public async Task<GetSessionSeatDto> CreateSessionSeatAsync(SaveSessionSeatDto dto)
    {
        var entity = mapper.Map<SessionSeat>(dto);
        context.SessionSeats.Add(entity);
        
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            throw new InvalidOperationException("This seat is already booked for this session");
        }

        return mapper.Map<GetSessionSeatDto>(entity);
    }

    public async Task<PagedResult<GetSessionSeatDto>> GetAllSessionSeatsAsync(int page, int size)
    {
        var query = context.SessionSeats.AsQueryable();
        var total = await query.CountAsync();
        var entities = await query
            .AsNoTracking()
            .Skip(page * size)
            .Take(size)
            .Select(s => mapper.Map<GetSessionSeatDto>(s))
            .ToListAsync();

        return new PagedResult<GetSessionSeatDto>(entities, total, page, size);
    }

    public async Task<GetSessionSeatDto> GetSessionSeatAsync(long id)
    {
        var entity = await context.SessionSeats
                         .AsNoTracking()
                         .FirstOrDefaultAsync(ss => ss.Id == id)
                           ?? throw new KeyNotFoundException($"SessionSeats with id {id} not found");
        return mapper.Map<GetSessionSeatDto>(entity);
    }

    public async Task<GetSessionSeatDto> UpdateSessionSeatAsync(long id, SaveSessionSeatDto dto)
    {
        var entity = await context.SessionSeats.FindAsync(id)
                           ?? throw new KeyNotFoundException($"SessionSeats with id {id} not found");

        entity.SessionId = dto.SessionId;
        entity.SeatId = dto.SeatId;
        entity.CustomerName = dto.CustomerName;
        entity.Contact = dto.Contact;

        await context.SaveChangesAsync();
        return mapper.Map<GetSessionSeatDto>(entity);
    }

    public async Task DeleteSessionSeatAsync(long id)
    {
        var rowsAffected = await context.SessionSeats
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Session seat with id {id} not found");
        }
    }
}