using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Order;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Places a new order
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] placeOrderDTO placeOrder)
        {
            var shippingId = placeOrder.shippindId;

            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                return Unauthorized(new { Message = "User not logged in" });
            }

            var response = await _orderService.PlaceOrder(shippingId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Retrieves an order by its ID
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var response = await _orderService.GetOrderByIdAsync(orderId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Retrieves all orders for a specific user
        [HttpGet("user-orders")]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                return Unauthorized(new { Message = "User not logged in" });
            }

            var userId = int.Parse(userContext);
            var response = await _orderService.GetOrdersByUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Deletes an order by its ID
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var response = await _orderService.DeleteOrderAsync(orderId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
