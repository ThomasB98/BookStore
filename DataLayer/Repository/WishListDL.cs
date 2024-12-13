using AutoMapper;
using DataLayer.Constants.DBContext;
using DataLayer.Exceptions;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.WishList_wishListItem;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DataLayer.Repository
{
    public class WishListDL : IwishList
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly HttpContextAccessor _httpContextAccessor;

        public WishListDL(IMapper mapper, DataContext dataContext, HttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseBody<WishListResponseDto>> AddToWishListAsync(int bookId)
        {
            var book=await _dataContext.Book.FirstOrDefaultAsync(b=>b.Id==bookId);
            if (book == null)
            {
                throw new BookNotFoundException("Book id invalid");
            }
            var userIdContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdContext);
            if (userId == null)
            {
                throw new UserNotLoggedInException("User Not logged in");
            }
            var UserExists = await _dataContext.User.FirstOrDefaultAsync(u => u.Id == userId);

            if(UserExists == null)
            {
                throw new UserNotFoundException("User not found");
            }

            var wishlist = await _dataContext.WishList.FirstOrDefaultAsync(u=>u.userId == userId);
            if (wishlist == null)
            {
                WishList wl = new WishList()
                {
                    userId = userId,
                    CreatedDate = DateTime.UtcNow
                };
                _dataContext.WishList.Add(wl);

                wishListItem wli = new wishListItem()
                {
                    WishListId = wl.id,
                    bookId = bookId
                };

                _dataContext.wishListItem.Add(wli);

                await _dataContext.SaveChangesAsync();

                var wishListDto = _mapper.Map<WishListResponseDto>(wl);

                return new ResponseBody<WishListResponseDto>
                {
                    Data= wishListDto,
                    Success= true,
                    Message="Book added to wish list",
                    StatusCode=HttpStatusCode.Created
                };

            }
            else
            {
                wishListItem wli = new wishListItem()
                {
                    WishListId = wishlist.id,
                    bookId = bookId
                };

                _dataContext.wishListItem.Add(wli);

                await _dataContext.SaveChangesAsync();

                var wishListDto = _mapper.Map<WishListResponseDto>(wishlist);

                return new ResponseBody<WishListResponseDto>
                {
                    Data = wishListDto,
                    Success = true,
                    Message = "Book added to wish list",
                    StatusCode = HttpStatusCode.Created
                };
            }
        }

        public Task<ResponseBody<bool>> ClearWishListAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<WishListResponseDto>> GetWishListAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<IEnumerable<WishListItemResponseDto>>> GetWishListItemsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<bool>> IsBookInWishListAsync(int userId, int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<bool>> RemoveFromWishListAsync(int wishListItemId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<WishListResponseDto>> UpdateWishlistAsync(int wishlistId, UpdateWishListDto updateWishListDto)
        {
            throw new NotImplementedException();
        }
    }
}
