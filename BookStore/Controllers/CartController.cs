using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Cart_CartItem;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Adds an item to the cart
        [HttpPost("add")]
        public async Task<IActionResult> AddItemToCart([FromBody] ModelLayer.DTO.Cart_CartItem.CartItemCreateDto cartItemDto)
        {
            var response = await _cartService.AddItemToCartAsync(cartItemDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Removes an item from the cart
        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            var response = await _cartService.RemoveItemFromCartAsync(cartItemId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Updates the quantity of a cart item
        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateCartItemQuantity([FromBody] ModelLayer.DTO.Cart_CartItem.CartItemUpdateDto updateDto)
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                return Unauthorized(new { Message = "User not logged in" });
            }

            var userId = int.Parse(userContext);
            var response = await _cartService.UpdateCartItemQuantityAsync(userId, updateDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Retrieves the cart for a specific user
        [HttpGet("user-cart")]
        public async Task<IActionResult> GetCartByUserId()
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                return Unauthorized(new { Message = "User not logged in" });
            }

            var userId = int.Parse(userContext);
            var response = await _cartService.GetCartByUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Clears all items from a cart
        [HttpDelete("clear/{cartId}")]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            var response = await _cartService.ClearCartAsync(cartId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Checks if a book is already in the cart
        [HttpGet("is-book-in-cart/{bookId}")]
        public async Task<IActionResult> IsBookInCart(int bookId)
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                return Unauthorized(new { Message = "User not logged in" });
            }

            var userId = int.Parse(userContext);
            var response = await _cartService.IsBookInCartAsync(userId, bookId);
            return Ok(response);
        }

        // Calculates the total cart price
        [HttpGet("total-price")]
        public async Task<IActionResult> CalculateCartTotal()
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                return Unauthorized(new { Message = "User not logged in" });
            }

            var userId = int.Parse(userContext);
            var totalPrice = await _cartService.CalculateCartTotalAsync(userId);
            return Ok(new { TotalPrice = totalPrice });
        }
    }
}
