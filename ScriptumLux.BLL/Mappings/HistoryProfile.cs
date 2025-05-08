using AutoMapper;
using ScriptumLux.BLL.DTOs.History;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            // Entity -> DTO
            CreateMap<History, HistoryDto>();

            // CreateDto -> Entity (preserve UserId, MovieId, ViewedAt)
            CreateMap<HistoryCreateDto, History>()
                .ForMember(dest => dest.User,   opt => opt.Ignore())
                .ForMember(dest => dest.Movie,  opt => opt.Ignore());

            // UpdateDto -> Entity (only ViewedAt)
            CreateMap<HistoryUpdateDto, History>()
                .ForMember(dest => dest.ViewedAt, opt => opt.Condition(src => src.ViewedAt.HasValue))
                .ForAllMembers(opt => opt.Ignore()); // Corrected this line
        }
    }
}
