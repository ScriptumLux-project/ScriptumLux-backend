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

            // Entity → DTO
            CreateMap<Comment, CommentDto>();

            // Update DTO → Entity
            CreateMap<CommentUpdateDto, Comment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}