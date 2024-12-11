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

                if (changes > 0)
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

            if (isExists != null)
            {
                throw new UserNotFoundException("User Not found/Invalid email");
            }
        }

        public Task<ResponseBody<bool>> ActivateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<bool>> DeactivateUserAsync(int userId)
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
