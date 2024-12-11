using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.User;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IUser
    {
        Task<ResponseBody<UserResponseDto>> CreateUserAsync(UserRegistrationDto userDto);

        Task<ResponseBody<UserResponseDto>> UpdateUserAsync(UserUpdateDto UserUpdateDto);

        Task<ResponseBody<bool>> DeleteUserAsync(int userId);

        Task<ResponseBody<UserResponseDto>> GetUserByIdAsync(int userId);

        Task<ResponseBody<UserResponseDto>> GetUserByEmailAsync(string email);

        Task<ResponseBody<ICollection<User>>> GetAllUsersAsync();

        Task<ResponseBody<bool>> UserExistsByEmailAsync(string email);

        Task<ResponseBody<bool>> ActivateUserAsync(int userId);

        Task<ResponseBody<bool>> DeactivateUserAsync(int userId);


    }
}
