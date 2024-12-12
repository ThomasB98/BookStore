using AutoMapper;
using ModelLayer.DTO.Cart_CartItem;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile() {

            CreateMap<Cart, CartResponseDto>();
            CreateMap<CartResponseDto, Cart>();

            CreateMap<Cart,CartUpdateDto>();
            CreateMap<CartUpdateDto,Cart>();
        }
    }
}
