using AutoMapper;
using ScriptumLux.BLL.DTOs.Comment;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentCreateDto, Comment>()
                .ForMember(c => c.CommentId, opt => opt.Ignore())
                .ForMember(c => c.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(c => c.Movie, opt => opt.Ignore())
                .ForMember(c => c.User, opt => opt.Ignore());

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name)) 
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title)); 

            CreateMap<CommentUpdateDto, Comment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}