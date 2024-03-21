using AutoMapper;
using ReservationSystem.WebApi.DTOs;
using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Mappers
{
    public class ReservationSystemMapper : Profile 
    {
        public ReservationSystemMapper()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<LocationSpotDto, LocationSpot>().ReverseMap();
            CreateMap<ReservationDto, Reservation>().ReverseMap();
        }       
    }
}

