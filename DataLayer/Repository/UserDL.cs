using AutoMapper;
using DataLayer.Constants.DBContext;
using DataLayer.Exceptions;
using DataLayer.Interfaces;
using DataLayer.Utilities.Hasher;
using DataLayer.Utilities.ResponseBody;
using Microsoft.EntityFrameworkCore;
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

        public UserDL(DataContext context, IMapper mapper,IPassHasher passHasher) {
            _context = context;
            _mapper = mapper;
            _passHasher = passHasher;
        }


        public async Task<ResponseBody<UserResponseDto>> CreateUserAsync(UserRegistrationDto userDto)
        {
                var isExists = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(userDto.Email));

                if (isExists != null)
                {
                    throw new UserAllredyExistsException("Email Alredy Exists");
                }
                var user = _mapper.Map<User>(userDto);
                user.CreatedDate= DateTime.Now;
                user.Password=_passHasher.encrypt(userDto.Password);
                await _context.User.AddAsync(user);

                int changes = await _context.SaveChangesAsync();

                if (changes < 0)
                {
                    throw new DatabaseOperationException("DataBase error");
                }
                var userRespo = _mapper.Map<UserResponseDto>(user);

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
            var isExists = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (isExists == null)
            {
                throw new UserNotFoundException("User Not found/Invalid email");
            }

            _context.User.Remove(isExists);

            int chages = await _context.SaveChangesAsync();
            if (chages > 0)
            {
                throw new DatabaseOperationException("DataBase error");
            }

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
            var userCollection =await _context.User.ToListAsync();

            var UserDto=_mapper.Map<ICollection<UserResponseDto>>(userCollection);

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
            var user = _context.User.FirstOrDefaultAsync(u=>u.Email.Equals(email));

            if (user == null)
            {
                throw new UserNotFoundException($"{email} not found.");
            }

            var userDto=_mapper.Map<UserResponseDto>(user);

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
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException("Invalid user Id");
            }

            user.IsActive = true;

            int changes = await _context.SaveChangesAsync();

            if (changes < 0)
            {
                throw new DatabaseOperationException("DataBase error");
            }

            return new ResponseBody<bool>
            {
                Data= true,
                Success=true,
                Message= "User active",
                StatusCode= HttpStatusCode.OK,
            };
        }

        public async Task<ResponseBody<bool>> DeactivateUserAsync(int userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException("Invalid user Id");
            }

            user.IsActive = true;

            int changes = await _context.SaveChangesAsync();

            if (changes < 0)
            {
                throw new DatabaseOperationException("DataBase error");
            }

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
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if(user == null)
            {
                throw new UserNotFoundException("Invalid user Id");
            }

            var userDto = _mapper.Map<UserResponseDto>(user);

            return new ResponseBody<UserResponseDto>
            {
                Data= userDto,
                Message="user Entity",
                StatusCode = HttpStatusCode.OK,
                Success = true
            };


        }

        public async Task<ResponseBody<UserResponseDto>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email.Equals(userUpdateDto.Email));

            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            user = _mapper.Map<User>(userUpdateDto);

            int changes = await _context.SaveChangesAsync();

            if(changes > 0)
            {
                var userDto = _mapper.Map<UserResponseDto>(user);
                return new ResponseBody<UserResponseDto>
                {
                    Data = userDto,
                    Message = "User Updaate Successfull",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                };
            }

            throw new DatabaseOperationException("Data Base error");

        }

        public async Task<ResponseBody<bool>> UserExistsByEmailAsync(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u=>u.Email.Equals(email));

            if(user == null)
            {
                throw new UserNotFoundException("User not found");
            }
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
