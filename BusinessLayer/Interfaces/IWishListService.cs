using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.WishList_wishListItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IWishListService
    {
        // Get wishlist details
        Task<ResponseBody<WishListResponseDto>> GetWishListAsync();

        // Add a book to wishlist
        Task<ResponseBody<WishListResponseDto>> AddToWishListAsync(int bookId);

        // Remove a book from wishlist
        Task<ResponseBody<bool>> RemoveFromWishListAsync(int wishListItemId);

        // Get all wishlist items for a user
        Task<ResponseBody<IEnumerable<WishListItemResponseDto>>> GetWishListItemsAsync();

        // Check if a book is already in the wishlist
        Task<ResponseBody<bool>> IsBookInWishListAsync(int bookId);

        // Clear entire wishlist
        Task<ResponseBody<bool>> ClearWishListAsync();
    }
}
