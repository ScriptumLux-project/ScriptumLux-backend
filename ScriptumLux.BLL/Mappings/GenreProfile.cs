using AutoMapper;
using ScriptumLux.BLL.DTOs.Genre;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<Genre, GenreCreateDto>().ReverseMap();
            CreateMap<Genre, GenreUpdateDto>().ReverseMap();
        }
    }
}