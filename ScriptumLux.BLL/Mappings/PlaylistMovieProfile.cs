using AutoMapper;
using ScriptumLux.BLL.DTOs.PlaylistMovie;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class PlaylistMovieProfile : Profile
    {
        public PlaylistMovieProfile()
        {
            CreateMap<PlaylistMovie, PlaylistMovieDto>();
            CreateMap<PlaylistMovie, PlaylistMovieCreateDto>().ReverseMap();
            CreateMap<PlaylistMovie, PlaylistMovieUpdateDto>().ReverseMap();
        }
    }
}