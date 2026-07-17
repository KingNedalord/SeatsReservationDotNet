using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.Session;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class SessionService(AppDbContext context, IMapper mapper) : ISessionService
{
    public async Task<GetSessionDto> CreateSessionAsync(SaveSessionDto dto)
    {
        var entity = mapper.Map<Session>(dto);
        context.Sessions.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<GetSessionDto>(entity);
    }

    public async Task<PagedResult<GetSessionDto>> GetAllSessionsAsync(int page, int size)
    {
        var query = context.Sessions.AsNoTracking();
        var total = await query.CountAsync();
        var entities = await query
            .OrderBy(s => s.Date).ThenBy(s => s.Time).ThenBy(s => s.Title)
            .Skip(page * size)
            .Take(size)
            .Select(s => mapper.Map<GetSessionDto>(s))
            .ToListAsync();
        return new PagedResult<GetSessionDto>(entities, total, page, size);
    }

    public async Task<GetSessionDto> GetSessionAsync(long id)
    {
        var entity = await context.Sessions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new KeyNotFoundException($"Session with id {id} not found");
        return mapper.Map<GetSessionDto>(entity);
    }

    public async Task<GetSessionDto> UpdateSessionAsync(long id, SaveSessionDto dto)
    {
        var entity = await context.Sessions.FindAsync(id)
            ?? throw new KeyNotFoundException($"Session with id {id} not found");

        entity.Title = dto.Title;
        entity.Date = dto.Date;
        entity.Time = dto.Time;
        entity.Language = dto.Language;
        entity.Format = dto.Format;

        await context.SaveChangesAsync();
        return mapper.Map<GetSessionDto>(entity);
    }

    public async Task DeleteSessionAsync(long id)
    {
        var entity = await context.Sessions.FindAsync(id)
            ?? throw new KeyNotFoundException($"Session with id {id} not found");
        context.Sessions.Remove(entity);
        await context.SaveChangesAsync();
    }
}
