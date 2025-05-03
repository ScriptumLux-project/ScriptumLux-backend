using AutoMapper;
using ScriptumLux.BLL.DTOs.History;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<History, HistoryDto>();
            CreateMap<History, HistoryCreateDto>().ReverseMap();
            CreateMap<History, HistoryUpdateDto>().ReverseMap();
        }
    }
}