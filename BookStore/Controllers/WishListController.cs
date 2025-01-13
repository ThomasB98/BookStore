using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.WishList_wishListItem;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost]
        public async Task<IActionResult> AddToWishList([FromBody] ModelLayer.DTO.WishList_wishListItem.AddToWishListDTO addToWishList)
        {
            var bookId = addToWishList.bookId;
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
