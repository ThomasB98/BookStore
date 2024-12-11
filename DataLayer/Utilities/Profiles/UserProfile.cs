using AutoMapper;
using ModelLayer.DTO.User;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {

            CreateMap<User, UserResponseDto>();
            CreateMap<UserResponseDto, User>();

            
            CreateMap<User, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, User>();

            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
