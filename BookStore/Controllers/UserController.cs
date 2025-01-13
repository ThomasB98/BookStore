using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.User;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] ModelLayer.DTO.User.UserRegistrationDto userDto)
        {
            var result =await _userService.CreateUserAsync(userDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] ModelLayer.DTO.User.UserLoginDto loginDto)
        {
            var result=await _userService.Login(loginDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("{userId}")]
        //[Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("email/{email}")]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("all")]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{email}")]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var result = await _userService.DeleteUserAsync(email);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPut("activate/{userId}")]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> ActivateUser(int userId)
        {
            var result = await _userService.ActivateUserAsync(userId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("deactivate/{userId}")]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> DeactivateUser(int userId)
        {
            var result = await _userService.DeactivateUserAsync(userId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("exists/{email}")]
        [Authorize(Policy = "RoleAdmin")]
        public async Task<IActionResult> UserExists(string email)
        {
            var result = await _userService.UserExistsByEmailAsync(email);

            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
