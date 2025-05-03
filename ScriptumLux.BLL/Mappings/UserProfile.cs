using AutoMapper;
using ScriptumLux.BLL.DTOs.User; 

using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}