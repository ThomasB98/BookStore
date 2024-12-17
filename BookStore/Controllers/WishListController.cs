using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishListController : Controller
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetWishList()
        {
            var response = await _wishListService.GetWishListAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // GET
        [HttpGet("items")]
        public async Task<IActionResult> GetWishListItems()
        {
            var response = await _wishListService.GetWishListItemsAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // POST
        [HttpPost("{bookId}")]
        public async Task<IActionResult> AddToWishList(int bookId)
        {
            var response = await _wishListService.AddToWishListAsync(bookId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // DELETE
        [HttpDelete("item/{wishListItemId}")]
        public async Task<IActionResult> RemoveFromWishList(int wishListItemId)
        {
            var response = await _wishListService.RemoveFromWishListAsync(wishListItemId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // GET
        [HttpGet("check/{bookId}")]
        public async Task<IActionResult> IsBookInWishList(int bookId)
        {
            var response = await _wishListService.IsBookInWishListAsync(bookId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // DELETE
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearWishList()
        {
            var response = await _wishListService.ClearWishListAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
