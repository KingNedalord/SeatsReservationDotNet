using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeatsReservationDotNet.Data;
using SeatsReservationDotNet.DTOs;
using SeatsReservationDotNet.DTOs.MovieDto;
using SeatsReservationDotNet.Entities;
using SeatsReservationDotNet.Services.Interfaces;

namespace SeatsReservationDotNet.Services;

public class MovieService(AppDbContext context, IMapper mapper) : IMovieService
{
    public async Task<GetMovieDto> CreateMovieAsync(SaveMovieDto dto)
    {
        var entity = mapper.Map<Movie>(dto);
        context.Movies.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<GetMovieDto>(entity);
    }

    public async Task<PagedResult<GetMovieDto>> GetAllMoviesAsync(int page, int size)
    {
        var query = context.Movies.AsQueryable();
        var total = await query.CountAsync();
        var entities = await query
            .AsNoTracking()
            .Skip(page * size)
            .Take(size)
            .Select(m => mapper.Map<GetMovieDto>(m))
            .ToListAsync();

        return new PagedResult<GetMovieDto>(entities, total, page, size);
    }

    public async Task<GetMovieDto> GetMovieAsync(long id)
    {
        var seat = await context.Movies
                       .AsNoTracking()
                       .FirstOrDefaultAsync(m => m.Id == id)
                   ?? throw new KeyNotFoundException($"Movie with id {id} not found");
        return mapper.Map<GetMovieDto>(seat);
    }

    public async Task<GetMovieDto> UpdateMovieAsync(long id, SaveMovieDto dto)
    {
        var entity = await context.Movies.FindAsync(id)
                   ?? throw new KeyNotFoundException($"Movie with id {id} not found");

        entity.Title = dto.Title;
        entity.DurationMinutes = dto.DurationMinutes;
        entity.AgeRating = dto.AgeRating;
        entity.Rating = dto.Rating;
        entity.PosterUrl = dto.PosterUrl;
        entity.ReleaseYear = dto.ReleaseYear;
        entity.Description = dto.Description;

        await context.SaveChangesAsync();
        return mapper.Map<GetMovieDto>(entity);
    }

    public async Task DeleteMovieAsync(long id)
    {
        var rowsAffected = await context.Movies
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Movie with id {id} not found");
        }
    }
}