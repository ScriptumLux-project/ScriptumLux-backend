using AutoMapper;
using ScriptumLux.BLL.DTOs.Playlist;
using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            CreateMap<Playlist, PlaylistDto>();
            CreateMap<Playlist, PlaylistCreateDto>().ReverseMap();
            CreateMap<Playlist, PlaylistUpdateDto>().ReverseMap();
        }
    }
}