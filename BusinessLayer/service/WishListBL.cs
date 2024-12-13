using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.WishList_wishListItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.service
{
    public class WishListBL : IWishListService
    {
        private readonly IwishList _wishrepo;
        public WishListBL(IwishList wishrepo)
        {
            _wishrepo = wishrepo;
        }

        public Task<ResponseBody<WishListResponseDto>> AddToWishListAsync(int bookId)
        {
            return _wishrepo.AddToWishListAsync(bookId);
        }

        public Task<ResponseBody<bool>> ClearWishListAsync()
        {
            return _wishrepo.ClearWishListAsync();
        }

        public Task<ResponseBody<WishListResponseDto>> GetWishListAsync()
        {
            return _wishrepo.GetWishListAsync();
        }

        public Task<ResponseBody<IEnumerable<WishListItemResponseDto>>> GetWishListItemsAsync()
        {
            return _wishrepo.GetWishListItemsAsync();
        }

        public Task<ResponseBody<bool>> IsBookInWishListAsync(int bookId)
        {
            return _wishrepo.IsBookInWishListAsync(bookId);
        }

        public Task<ResponseBody<bool>> RemoveFromWishListAsync(int wishListItemId)
        {
            return _wishrepo.RemoveFromWishListAsync(wishListItemId);
        }
    }
}
