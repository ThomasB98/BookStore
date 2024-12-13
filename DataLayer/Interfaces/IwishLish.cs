using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.WishList_wishListItem;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IwishList
    {
        // Get wishlist details
        Task<ResponseBody<WishListResponseDto>> GetWishListAsync(int userId);

        // Add a book to wishlist
        Task<ResponseBody<WishListResponseDto>> AddToWishListAsync(int bookId);

        // Remove a book from wishlist
        Task<ResponseBody<bool>> RemoveFromWishListAsync(int wishListItemId);

        // Get all wishlist items for a user
        Task<ResponseBody<IEnumerable<WishListItemResponseDto>>> GetWishListItemsAsync(int userId);

        // Check if a book is already in the wishlist
        Task<ResponseBody<bool>> IsBookInWishListAsync(int userId, int bookId);

        // Clear entire wishlist
        Task<ResponseBody<bool>> ClearWishListAsync(int userId);

        // Method to update wishlist
        Task<ResponseBody<WishListResponseDto>> UpdateWishlistAsync(int wishlistId, UpdateWishListDto updateWishListDto);

    }
}
