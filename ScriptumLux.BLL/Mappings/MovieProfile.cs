using AutoMapper;
using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieCreateDto>().ReverseMap();
            CreateMap<Movie, MovieUpdateDto>().ReverseMap();
        }
    }
}