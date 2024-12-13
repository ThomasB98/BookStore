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

        public async Task<ResponseBody<bool>> ClearWishListAsync()
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userContext == null)
            {
                throw new UserNotLoggedInException("user is not loggedin");
            }

            var userId = int.Parse(userContext);

            var wishList = await _dataContext.WishList.FirstOrDefaultAsync(wl => wl.userId == userId);

            if(wishList == null)
            {
                return new ResponseBody<bool>
                {
                    Data = true,
                    Success = true,
                    Message = "WishList Clear",
                    StatusCode = HttpStatusCode.OK
                };
            }

            var wishListItems = await _dataContext.wishListItem
                .Where(wi => wi.WishListId == wishList.id)
                .ToListAsync();

            _dataContext.wishListItem.RemoveRange(wishListItems);
            await _dataContext.SaveChangesAsync();

            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "WishList Clear",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseBody<WishListResponseDto>> GetWishListAsync()
        {
            var userContext= _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userContext == null )
            {
                throw new UserNotLoggedInException("user is not loggedin");
            }

            var userId = int.Parse(userContext);

            var wishList=await _dataContext.WishList.FirstOrDefaultAsync(wl=>wl.userId==userId);

            if(wishList == null)
            {
                WishList wl = new WishList()
                {
                    userId=userId,
                    CreatedDate = DateTime.Now,
                };

                var wishListDto=_mapper.Map<WishListResponseDto>(wl);

                return new ResponseBody<WishListResponseDto>
                {
                    Data = wishListDto,
                    Success = true,
                    Message = "user wishList",
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                var wishListDto = _mapper.Map<WishListResponseDto>(wishList);

                return new ResponseBody<WishListResponseDto>
                {
                    Data = wishListDto,
                    Success = true,
                    Message = "user wishList",
                    StatusCode = HttpStatusCode.OK
                };
            }
        }

        public async Task<ResponseBody<IEnumerable<WishListItemResponseDto>>> GetWishListItemsAsync()
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userContext == null)
            {
                throw new UserNotLoggedInException("user is not loggedin");
            }

            var userId = int.Parse(userContext);

            var wishList = await _dataContext.WishList
                .Include(w => w.WishListItems)
                    .ThenInclude(wi => wi.book)
                .FirstOrDefaultAsync(w => w.userId == userId);

            if (wishList == null)
            {
                return new ResponseBody<IEnumerable<WishListItemResponseDto>>
                {
                    Data = null,
                    Success = true,
                    Message = "no item in wish lish",
                    StatusCode = HttpStatusCode.OK
                };
            }

            var wishListDto = _mapper.Map<IEnumerable<WishListItemResponseDto>>(wishList);

            return new ResponseBody<IEnumerable<WishListItemResponseDto>>
            {
                Data = wishListDto,
                Success = true,
                Message = "Wishlist items retrieved successfully.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseBody<bool>> IsBookInWishListAsync(int bookId)
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userContext == null)
            {
                throw new UserNotLoggedInException("user is not loggedin");
            }
            var userId = int.Parse(userContext);

            var existingWishListItem = await _dataContext.wishListItem.Include(wi => wi.WishList)
                .FirstOrDefaultAsync(wi => wi.WishList.userId == userId && wi.bookId == bookId);

            if(existingWishListItem == null)
            {
                return new ResponseBody<bool>
                {
                    Data = false,
                    Success = true,
                    Message = "Book not present",
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "Book not present",
                StatusCode = HttpStatusCode.OK
            };

        }

        public async Task<ResponseBody<bool>> RemoveFromWishListAsync(int wishListItemId)
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userContext == null)
            {
                throw new UserNotLoggedInException("user is not loggedin");
            }
            var userId = int.Parse(userContext);

            var wishlist= await _dataContext.WishList.FirstOrDefaultAsync(wl=>wl.userId == userId);

            if(wishlist == null)
            {
                throw new NoWishListException();
            }

            var wishListItem = await _dataContext.wishListItem.FirstOrDefaultAsync(wli=>wli.Id==wishListItemId);

            if(wishListItem == null)
            {
                throw new BookNotFoundException("No such item");
            }

            _dataContext.wishListItem.Remove(wishListItem);

            await _dataContext.SaveChangesAsync();

            return new ResponseBody<bool>
            {
                Data= true,
                Success = true,
                Message = "Book removed",
                StatusCode= HttpStatusCode.OK
            };
        }
    }
}
