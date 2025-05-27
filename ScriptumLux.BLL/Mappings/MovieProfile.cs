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
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => (double)src.AverageRating)) // Исправление: маппим AverageRating на Rating
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
                .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.TotalRatings))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.PosterUrl))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            // Маппинг для создания фильма
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.MovieId, opt => opt.Ignore())
                .ForMember(dest => dest.GenreId, opt => opt.Ignore()) // Устанавливается в сервисе
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.TotalRatingSum, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.PlaylistMovies, opt => opt.Ignore())
                .ForMember(dest => dest.HistoryRecords, opt => opt.Ignore())
                .ForMember(dest => dest.Timecodes, opt => opt.Ignore());

            // Маппинг для обновления фильма
            CreateMap<MovieUpdateDto, Movie>()
                .ForMember(dest => dest.MovieId, opt => opt.Ignore())
                .ForMember(dest => dest.GenreId, opt => opt.Ignore()) // Устанавливается в сервисе
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore()) // Не обновляем рейтинг
                .ForMember(dest => dest.TotalRatings, opt => opt.Ignore())
                .ForMember(dest => dest.TotalRatingSum, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.PlaylistMovies, opt => opt.Ignore())
                .ForMember(dest => dest.HistoryRecords, opt => opt.Ignore())
                .ForMember(dest => dest.Timecodes, opt => opt.Ignore());

            // Маппинг комментариев
            CreateMap<ScriptumLux.DAL.Entities.Comment, ScriptumLux.BLL.DTOs.Comment.CommentDto>();
        }
    }
}