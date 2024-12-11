using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.User;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class UserBL : IUser
    {
        public Task<ResponseBody<bool>> ActivateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<UserResponseDto>> CreateUserAsync(UserRegistrationDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<bool>> DeactivateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<bool>> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<ICollection<User>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<UserResponseDto>> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<UserResponseDto>> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<UserResponseDto>> UpdateUserAsync(UserUpdateDto UserUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<bool>> UserExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
