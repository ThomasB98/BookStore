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
    public class WishLisProfile : Profile
    {
        public WishLisProfile() {
            CreateMap<WishList, UpdateWishListDto>();
            CreateMap<UpdateWishListDto, WishList>();

            CreateMap<WishList, WishListResponseDto>();
            CreateMap<WishListResponseDto, WishList>();
        }
    }
}
