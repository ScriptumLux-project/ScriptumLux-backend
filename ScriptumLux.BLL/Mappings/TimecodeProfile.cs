
using AutoMapper;
using ScriptumLux.BLL.DTOs.Timecode;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class TimecodeProfile : Profile
    {
        public TimecodeProfile()
        {
            // Entity -> DTO
            CreateMap<Timecode, TimecodeDto>();

            // CreateDto -> Entity (ignore Timestamp, will assign manually in service)
            CreateMap<TimecodeCreateDto, Timecode>()
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore());

            // UpdateDto -> Entity (ignore Timestamp, assign in service)
            CreateMap<TimecodeUpdateDto, Timecode>()
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore());
        }
    }
}