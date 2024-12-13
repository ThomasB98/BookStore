using AutoMapper;
using ModelLayer.DTO.WishList_wishListItem;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Profiles
{
    public class WishListItemProfile : Profile
    {
        public WishListItemProfile()
        {
            CreateMap<wishListItem, CreateWishListItemDto>();
            CreateMap<CreateWishListItemDto,wishListItem>();

            CreateMap<wishListItem, WishListItemResponseDto>();
            CreateMap<WishListItemResponseDto, wishListItem>();
        }
        

    }
}
