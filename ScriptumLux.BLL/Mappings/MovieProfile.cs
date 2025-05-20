using AutoMapper;
using ScriptumLux.BLL.DTOs.Movie;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            // Маппинг фильма с комментариями
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.PosterUrl))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            CreateMap<Movie, MovieCreateDto>().ReverseMap();
            CreateMap<Movie, MovieUpdateDto>().ReverseMap();

            // Маппинг комментариев
            CreateMap<ScriptumLux.DAL.Entities.Comment, ScriptumLux.BLL.DTOs.Comment.CommentDto>();
        }

    }
}