using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Cart_CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.service
{
    public class CartBL : ICartService
    {
        private readonly ICart _cartRepo;

        public CartBL(ICart cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public Task<ResponseBody<CartItemResponseDto>> AddItemToCartAsync(CartItemCreateDto cartItemDto)
        {
           return _cartRepo.AddItemToCartAsync(cartItemDto);    
        }

        public Task<float> CalculateCartTotalAsync(int userId)
        {
            return _cartRepo.CalculateCartTotalAsync(userId);
        }

        public Task<ResponseBody<CartResponseDto>> ClearCartAsync(int cartId)
        {
            return _cartRepo.ClearCartAsync(cartId);
        }

        public Task<ResponseBody<CartResponseDto>> GetCartByUserIdAsync(int userId)
        {
            return _cartRepo.GetCartByUserIdAsync(userId);
        }

        public Task<CartResponseDto> IsBookInCartAsync(int userId, int bookId)
        {
            return _cartRepo.IsBookInCartAsync(userId, bookId);
        }

        public Task<ResponseBody<CartResponseDto>> RemoveItemFromCartAsync(int cartItemId)
        {
            return _cartRepo.RemoveItemFromCartAsync(cartItemId);
        }

        public Task<ResponseBody<CartResponseDto>> UpdateCartItemQuantityAsync(int userId, CartItemUpdateDto updateDto)
        {
            return _cartRepo.UpdateCartItemQuantityAsync(userId, updateDto);
        }
    }
}
