using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Cart_CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICartService
    {
        //Adds a new item to the cart using CartItemCreateDto.
        Task<ResponseBody<CartItemResponseDto>> AddItemToCartAsync(CartItemCreateDto cartItemDto);

        //Removes a specific item from the cart using its ID.
        Task<ResponseBody<CartResponseDto>> RemoveItemFromCartAsync(int cartItemId);

        // Update cart item quantity
        Task<ResponseBody<CartResponseDto>> UpdateCartItemQuantityAsync(int userId, CartItemUpdateDto updateDto);

        //Retrieves the cart associated with a specific user.
        Task<ResponseBody<CartResponseDto>> GetCartByUserIdAsync(int userId);

        //Clears all items from the specified cart.
        Task<ResponseBody<CartResponseDto>> ClearCartAsync(int cartId);

        // Check if book is already in cart
        Task<CartResponseDto> IsBookInCartAsync(int userId, int bookId);

        // Calculate total cart price
        Task<float> CalculateCartTotalAsync(int userId);
    }
}
