using AutoMapper;
using ScriptumLux.BLL.DTOs.User; 

using ScriptumLux.DAL.Entities;

namespace ScriptumLux.BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>()
                .ForMember(u => u.UserId,       opt => opt.Ignore())
                .ForMember(u => u.PasswordHash, opt => opt.Ignore())
                .ForMember(u => u.PasswordSalt, opt => opt.Ignore())
                .ForMember(u => u.IsBanned,     opt => opt.MapFrom(_ => false));

            CreateMap<User, UserDto>();

            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}