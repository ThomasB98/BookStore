using AutoMapper;
using DataLayer.Constants.DBContext;
using DataLayer.Exceptions;
using DataLayer.Interfaces;
using DataLayer.Utilities.Hasher;
using DataLayer.Utilities.Logger;
using DataLayer.Utilities.ResponseBody;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.DTO.User;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class UserDL : IUser
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPassHasher _passHasher;
        private readonly ILoggerService _logger;

        public UserDL(DataContext context, IMapper mapper, IPassHasher passHasher, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _passHasher = passHasher;
            _logger = loggerService;
        }


        public async Task<ResponseBody<UserResponseDto>> CreateUserAsync(UserRegistrationDto userDto)
        {
            _logger.LogInformation($"Attempting to create user with email: {userDto.Email}");

            var isExists = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(userDto.Email));

            if (isExists != null)
            {
                _logger.LogWarning($"User registration attempt failed. Email already exists: {userDto.Email}");
                throw new UserAllredyExistsException("Email Alredy Exists");
            }
            var user = _mapper.Map<User>(userDto);
            user.CreatedDate = DateTime.Now;
            user.Password = _passHasher.encrypt(userDto.Password);
            await _context.User.AddAsync(user);

            int changes = await _context.SaveChangesAsync();

            if (changes <= 0)
            {
                _logger.LogError("Database operation failed during user creation");
                throw new DatabaseOperationException("DataBase error");
            }
            var userRespo = _mapper.Map<UserResponseDto>(user);
            _logger.LogInformation($"User created successfully. Email: {userDto.Email}, User ID: {userRespo.Id}");

            return new ResponseBody<UserResponseDto>
            {
                Data = userRespo,
                Success = true,
                Message = "User Registration sucessfull",
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<ResponseBody<bool>> DeleteUserAsync(string email)
        {
            _logger.LogInformation($"Attempting to delete user with email: {email}");
            var isExists = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (isExists == null)
            {
                _logger.LogWarning($"Delete user attempt failed. User not found: {email}");
                throw new UserNotFoundException("User Not found/Invalid email");
            }

            _context.User.Remove(isExists);

            int chages = await _context.SaveChangesAsync();
            if (chages <= 0)
            {
                _logger.LogError($"Database operation failed during user deletion. Email: {email}");
                throw new DatabaseOperationException("DataBase error");
            }
            _logger.LogInformation($"User deleted successfully. Email: {email}");
            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "User Deleted sucessfull",
                StatusCode = HttpStatusCode.OK
            };


        }

        public async Task<ResponseBody<ICollection<UserResponseDto>>> GetAllUsersAsync()
        {
            _logger.LogInformation("Retrieving all users");
            var userCollection = await _context.User.ToListAsync();



            var UserDto = _mapper.Map<ICollection<UserResponseDto>>(userCollection);
            _logger.LogInformation($"Retrieved {UserDto.Count} users");

            return new ResponseBody<ICollection<UserResponseDto>>
            {
                Data = UserDto,
                Success = true,
                Message = "user data",
                StatusCode = HttpStatusCode.OK
            };
        }


        public async Task<ResponseBody<UserResponseDto>> GetUserByEmailAsync(string email)
        {
            _logger.LogInformation($"Retrieving user by email: {email}");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user == null)
            {
                _logger.LogWarning($"User not found. Email: {email}");
                throw new UserNotFoundException($"{email} not found.");
            }

            var userDto = _mapper.Map<UserResponseDto>(user);
            _logger.LogInformation($"User retrieved successfully. Email: {email}, User ID: {userDto.Id}");

            return new ResponseBody<UserResponseDto>
            {
                Data = userDto,
                Message = "User Data",
                StatusCode = HttpStatusCode.OK,
                Success = true
            };
        }

        public async Task<ResponseBody<bool>> ActivateUserAsync(int userId)
        {
            _logger.LogInformation($"Attempting to activate user. User ID: {userId}");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning($"User activation failed. Invalid user ID: {userId}");
                throw new UserNotFoundException("Invalid user Id");
            }

            user.IsActive = true;

            int changes = await _context.SaveChangesAsync();

            if (changes < 0)
            {
                _logger.LogError($"Database operation failed during user activation. User ID: {userId}");
                throw new DatabaseOperationException("DataBase error");
            }

            _logger.LogInformation($"User activated successfully. User ID: {userId}");

            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "User active",
                StatusCode = HttpStatusCode.OK,
            };
        }

        public async Task<ResponseBody<bool>> DeactivateUserAsync(int userId)
        {
            _logger.LogInformation($"Attempting to deactivate user. User ID: {userId}");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning($"User deactivation failed. Invalid user ID: {userId}");
                throw new UserNotFoundException("Invalid user Id");
            }

            user.IsActive = true;

            int changes = await _context.SaveChangesAsync();

            if (changes < 0)
            {
                _logger.LogError($"Database operation failed during user deactivation. User ID: {userId}");
                throw new DatabaseOperationException("DataBase error");
            }

            _logger.LogInformation($"User deactivated successfully. User ID: {userId}");

            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "User Deactive",
                StatusCode = HttpStatusCode.OK,
            };
        }

        public async Task<ResponseBody<UserResponseDto>> GetUserByIdAsync(int userId)
        {
            _logger.LogInformation($"Retrieving user by ID: {userId}");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning($"User not found. User ID: {userId}");
                throw new UserNotFoundException("Invalid user Id");
            }

            var userDto = _mapper.Map<UserResponseDto>(user);
            _logger.LogInformation($"User retrieved successfully. User ID: {userId}, Email: {userDto.Email}");

            return new ResponseBody<UserResponseDto>
            {
                Data = userDto,
                Message = "user Entity",
                StatusCode = HttpStatusCode.OK,
                Success = true
            };


        }

        public async Task<ResponseBody<UserResponseDto>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            _logger.LogInformation($"Attempting to update user. Email: {userUpdateDto.Email}");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(userUpdateDto.Email));

            if (user == null)
            {
                _logger.LogWarning($"User update failed. User not found: {userUpdateDto.Email}");
                throw new UserNotFoundException("User not found");
            }

            user = _mapper.Map<User>(userUpdateDto);

            int changes = await _context.SaveChangesAsync();

            if (changes > 0)
            {
                var userDto = _mapper.Map<UserResponseDto>(user);
                _logger.LogInformation($"User updated successfully. Email: {userUpdateDto.Email}, User ID: {userDto.Id}");

                return new ResponseBody<UserResponseDto>
                {
                    Data = userDto,
                    Message = "User Updaate Successfull",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                };
            }
            _logger.LogError($"Database operation failed during user update. Email: {userUpdateDto.Email}");
            throw new DatabaseOperationException("Data Base error");

        }

        public async Task<ResponseBody<bool>> UserExistsByEmailAsync(string email)
        {
            _logger.LogInformation($"Checking if user exists. Email: {email}");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user == null)
            {
                _logger.LogWarning($"User does not exist. Email: {email}");
                throw new UserNotFoundException("User not found");
            }

            _logger.LogInformation($"User exists. Email: {email}");

            return new ResponseBody<bool>
            {
                Data = true,
                Message = "user Exists",
                StatusCode = HttpStatusCode.OK,
                Success = true
            };
        }

    }  
}
