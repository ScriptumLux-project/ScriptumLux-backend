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

            // DTO -> Entity (ignore navigation and keys)
            CreateMap<HistoryCreateDto, History>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Movie, opt => opt.Ignore());

            CreateMap<HistoryUpdateDto, History>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
