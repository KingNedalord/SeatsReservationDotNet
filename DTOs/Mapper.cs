using AutoMapper;
using SeatsReservationDotNet.DTOs.CinemaDto;
using SeatsReservationDotNet.DTOs.HallDto;
using SeatsReservationDotNet.DTOs.MovieDto;
using SeatsReservationDotNet.DTOs.PriceCategoryDto;
using SeatsReservationDotNet.DTOs.SeatDto;
using SeatsReservationDotNet.DTOs.Session;
using SeatsReservationDotNet.DTOs.SessionSeatsDto;

namespace SeatsReservationDotNet.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Cinema DTO
        CreateMap<SaveCinemaDto, Entities.Cinema>();
        CreateMap<Entities.Cinema, GetCinemaDto>();

        // Hall DTO
        CreateMap<SaveHallDto, Entities.Hall>();
        CreateMap<Entities.Hall, GetHallDto>();

        // Movie DTO
        CreateMap<SaveMovieDto, Entities.Movie>();
        CreateMap<Entities.Movie, GetMovieDto>();

        // PriceCategory DTO
        CreateMap<SavePriceCategoryDto, Entities.PriceCategory>();
        CreateMap<Entities.PriceCategory, GetPriceCategoryDto>();

        // SessionSeat DTO
        CreateMap<SaveSessionSeatDto, Entities.SessionSeat>();
        CreateMap<Entities.SessionSeat, GetSessionSeatDto>();

        // Seat DTO
        CreateMap<SaveSeatDto, Entities.Seat>();
        CreateMap<Entities.Seat, GetSeatDto>();

        // Session
        CreateMap<SaveSessionDto, Entities.Session>();
        CreateMap<Entities.Session, GetSessionDto>();
    }
}