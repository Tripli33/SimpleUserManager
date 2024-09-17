using AutoMapper;
using SimpleUserManager.DTOs;
using SimpleUserManager.Models;

namespace SimpleUserManager.Utilities.AutoMapperProfiles
{
    public class SimpleUserManagerAutoMapperProfile : Profile
    {
        public SimpleUserManagerAutoMapperProfile()
        {
            CreateMap<User, UserManipulationDto>();
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
