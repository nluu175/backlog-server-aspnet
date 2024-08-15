using AutoMapper;
using BacklogAPI.Models;
using BacklogAPI.DTOs;

namespace BacklogAPI.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Game, GameDto>();
            CreateMap<Backlog, BacklogDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game.Name))
                .ForMember(dest => dest.SteamAppId, opt => opt.MapFrom(src => src.Game.SteamAppId));
        }
    }
}