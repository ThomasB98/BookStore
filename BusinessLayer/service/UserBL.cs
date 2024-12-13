using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.service
{
    public class UserBL : IUserService
    {
        private readonly IUser _userRepo;

        public UserBL(IUser userRepo)
        {
            _userRepo = userRepo;
        }
        public Task<ResponseBody<bool>> ActivateUserAsync(int userId)
        {
            return _userRepo.ActivateUserAsync(userId);
        }

        public Task<ResponseBody<UserResponseDto>> CreateUserAsync(UserRegistrationDto userDto)
        {
            return _userRepo.CreateUserAsync(userDto);
        }

        public Task<ResponseBody<bool>> DeactivateUserAsync(int userId)
        {
            return _userRepo.DeactivateUserAsync(userId);
        }

        public Task<ResponseBody<bool>> DeleteUserAsync(string email)
        {
            return _userRepo.DeleteUserAsync(email);
        }

        public Task<ResponseBody<ICollection<UserResponseDto>>> GetAllUsersAsync()
        {
            return _userRepo.GetAllUsersAsync();
        }

        public Task<ResponseBody<UserResponseDto>> GetUserByEmailAsync(string email)
        {
            return _userRepo.GetUserByEmailAsync(email);
        }

        public Task<ResponseBody<UserResponseDto>> GetUserByIdAsync(int userId)
        {
            return (_userRepo.GetUserByIdAsync(userId));
        }

        public Task<ResponseBody<string>> Login(UserLoginDto loginDto)
        {
            return _userRepo.Login(loginDto);
        }

        public Task<ResponseBody<bool>> UserExistsByEmailAsync(string email)
        {
            return _userRepo.UserExistsByEmailAsync(email);
        }
    }
}
