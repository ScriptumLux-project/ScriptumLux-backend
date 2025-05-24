using AutoMapper;
using ScriptumLux.BLL.DTOs.Review;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>();
        }
    }
}