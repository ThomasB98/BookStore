using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Shipping;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : Controller
    {
        private readonly IShippingService _shippingService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ShippingController(IShippingService shippingService, IHttpContextAccessor httpContextAccessor)
        {
            _shippingService = shippingService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Adds a new shipping address
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddAddress([FromBody] ModelLayer.DTO.Shipping.ShippingCreateDto shippingDto)
        {
            var response = await _shippingService.AddAddressAsync(shippingDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Deletes a shipping address by ID
        [HttpDelete("delete/{shippingId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAddress(int shippingId)
        {
            var response = await _shippingService.DeleteAddressAsync(shippingId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Retrieves a shipping address by ID
        [HttpGet("get/{shippingId}")]
        [Authorize]
        public async Task<IActionResult> GetAddressByID(int shippingId)
        {
            var response = await _shippingService.GetAddressByIDAsync(shippingId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Retrieves all shipping addresses for the logged-in user
        [HttpGet("user-addresses")]
        [Authorize]
        public async Task<IActionResult> GetAllAddressesByUserId()
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                return Unauthorized(new { Message = "User not logged in" });
            }

            var response = await _shippingService.getAllAddressByUserId();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Updates a shipping address
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] ShippingUpdateDto shippingDto)
        {
            var response = await _shippingService.updateAddress(shippingDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
