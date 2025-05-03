using AutoMapper;
using ScriptumLux.BLL.DTOs.Timecode;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class TimecodeProfile : Profile
    {
        public TimecodeProfile()
        {
            CreateMap<Timecode, TimecodeDto>();
            CreateMap<Timecode, TimecodeCreateDto>().ReverseMap();
            CreateMap<Timecode, TimecodeUpdateDto>().ReverseMap();
        }
    }
}