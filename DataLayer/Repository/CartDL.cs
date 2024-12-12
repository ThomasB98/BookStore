using AutoMapper;
using DataLayer.Constants.DBContext;
using DataLayer.Exceptions;
using DataLayer.Interfaces;
using DataLayer.Utilities.Logger;
using DataLayer.Utilities.ResponseBody;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Cart_CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ModelLayer.Model.Entity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DataLayer.Repository
{
    public class CartDL : ICart
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ILoggerService _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartDL(IUser user,IMapper mapper,
                      DataContext context,ILoggerService logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = user;
            _mapper = mapper;
            _logger = logger;   
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<ResponseBody<CartResponseDto>> GetCartByUserIdAsync(int userId)
        {
            _logger.LogInformation($"Checking if user exist in cartDL GetCartByUserIdAsync method ID:{userId}");
            var user= await _user.GetUserByIdAsync(userId);

            if(user == null)
            {
                _logger.LogError($"user doesn't exist! for ID:{userId}, error from cartDL GetCartByUserIdAsync method ");
                throw new UserNotFoundException("User Not Found");
            }

            _logger.LogInformation($"getting user cart UserId:{userId}, log from cartDL GetCartByUserIdAsync method ");
            var cart= await _context.Cart.FirstOrDefaultAsync(c=>c.userId == userId);

            var cartDto=_mapper.Map<CartResponseDto>(cart);

            _logger.LogInformation($"responce sent, log from cartDL GetCartByUserIdAsync method ");
            return new ResponseBody<CartResponseDto>
            {
                Data = cartDto,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "User cart"
            };
        }

        public async Task<ResponseBody<CartItemResponseDto>> AddItemToCartAsync(CartItemCreateDto cartItemDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation("Validate book exists and is in stock");
            var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == cartItemDto.BookId);

            if (book == null)
            {
                _logger.LogWarning($"Book with ID {cartItemDto.BookId} not found.");
                throw new BookNotFoundException($"Book with id {cartItemDto.BookId} dosen't exists");
            }
            if (book.stock < cartItemDto.Quantity)
            {
                _logger.LogWarning($"Insufficient stock for book ID {cartItemDto.BookId}. Requested: {cartItemDto.Quantity}, Available: {book.stock}");
                throw new InvalidOperationException("Insufficient stock");
            }
            var cart = await _context.Cart
                .Include(c => c.cartItems)
                .FirstOrDefaultAsync(c => c.userId == int.Parse(userId));

            if (cart == null)
            {
                _logger.LogInformation($"Creating a new cart for user ID {int.Parse(userId)}.");
                cart = new Cart
                {
                    userId = int.Parse(userId),
                    total = 0,
                    cartItems = new List<CartItem>()
                };
                _context.Cart.Add(cart);
            }

            // Check if the item already exists in the cart
            var existingCartItem = cart.cartItems?.FirstOrDefault(ci => ci.bookId == cartItemDto.BookId);

            if (existingCartItem != null)
            {
                _logger.LogInformation($"Updating quantity for existing cart item with book ID {cartItemDto.BookId}.");
                existingCartItem.quantity += cartItemDto.Quantity;
                existingCartItem.Price +=book.price * cartItemDto.Quantity;
            }
            else
            {
                _logger.LogInformation($"Adding new cart item for book ID {cartItemDto.BookId}.");
                var newCartItem = new CartItem
                {
                    Cart = cart,
                    bookId = cartItemDto.BookId,
                    quantity = cartItemDto.Quantity,
                    Price = book.price * cartItemDto.Quantity
                };
                cart.cartItems?.Add(newCartItem);
            }

            // Update cart total
            cart.total = cart.cartItems?.Sum(ci => ci.Price) ?? 0;

            await _context.SaveChangesAsync();

            // Map to response DTO
            var responseDto = _mapper.Map<CartItemResponseDto>(existingCartItem ?? cart.cartItems.Last());

            _logger.LogInformation("Item added to cart successfully.");

            return new ResponseBody<CartItemResponseDto>
            {
                Data = responseDto,
                Success = true,
                Message = "Item added to cart successfully.",
                StatusCode = HttpStatusCode.OK
            };

        }

        public async Task<ResponseBody<CartResponseDto>> ClearCartAsync(int cartId)
        {
            _logger.LogInformation($"checking if cart with Id {cartId} existsor not");
            var cart = _context.Cart.Include(c=>c.cartItems).FirstOrDefault(c=>c.id==cartId);

            if (cart == null)
            {
                _logger.LogError($"cart with id:{cartId} doesn't exists");
                throw new CartNotFoundException($"cart id {cartId} invalid");
            }

            if (cart.cartItems == null)
            {
                _logger.LogError($"cart with id:{cartId} is empty");
                throw new NoItemsInCart("Cart is empty");
            }
            _context.CartItem.RemoveRange(cart.cartItems);
            cart.total = 0;

            await _context.SaveChangesAsync();

            return await GetCartByUserIdAsync(cart.userId);

        }

        public async Task<ResponseBody<CartResponseDto>> RemoveItemFromCartAsync(int cartItemId)
        {
            var UserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userId=int.Parse(UserId);

            var cartItem = await _context.CartItem
               .Include(ci => ci.Cart)
               .FirstOrDefaultAsync(ci => ci.id == cartItemId && ci.Cart.userId == userId);

            if (cartItem == null)
            {
                throw new ItemNotFoundException("item not fonud");
            }

            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();

            // Recalculate cart total
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.userId == userId);

            if (cart != null)
            {
                cart.total = await CalculateCartTotalAsync(userId);
                await _context.SaveChangesAsync();
            }

            return await GetCartByUserIdAsync(userId);

        }

        //public Task<ResponseBody<CartResponseDto>> UpdateCartAsync(int cartId, CartUpdateDto cartDto)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ResponseBody<CartResponseDto>> UpdateCartItemQuantityAsync(int userId, CartItemUpdateDto updateDto)
        {
            var cartItem = await _context.CartItem
               .Include(ci => ci.Cart)
               .Include(ci => ci.book)
               .FirstOrDefaultAsync(ci => ci.id == updateDto.CartItemId && ci.Cart.userId == userId);

            if (cartItem == null)
            {
                throw new ItemNotFoundException("Item not found");
            }

            if (cartItem.book.stock < updateDto.NewQuantity)
            {
                throw new InsufficientStockException("Insufficient stock");
            }

            cartItem.quantity = updateDto.NewQuantity;

            // Update cart total
            var cart = cartItem.Cart;
            cart.total = await CalculateCartTotalAsync(userId);

            await _context.SaveChangesAsync();

            return await GetCartByUserIdAsync(userId);
        }

        public async Task<CartResponseDto> IsBookInCartAsync(int userId, int bookId)
        {
            var cart = await _context.Cart
                .Include(c => c.cartItems)
                    .ThenInclude(ci => ci.book)
                .FirstOrDefaultAsync(c => c.userId == userId);

            if (cart == null)
                throw new ArgumentException("Cart not found");

            return new CartResponseDto
            {
                Id = cart.id,
                UserId = cart.userId,
                Total = cart.total,
                CartItems = cart.cartItems?.Select(ci => new CartItemResponseDto
                {
                    Id = ci.id,
                    BookId = ci.bookId,
                    BookTitle = ci.book?.Title ?? string.Empty,
                    Quantity = ci.quantity,
                    Price = ci.book?.price ?? 0,
                    Total = ci.quantity * (ci.book?.price ?? 0)
                }).ToList() ?? new List<CartItemResponseDto>()
            };
        }

        public async Task<float> CalculateCartTotalAsync(int userId)
        {
            return await _context.CartItem
                .Where(ci => ci.Cart.userId == userId)
                .SumAsync(ci => ci.quantity * ci.Price);
        }
    }
}
