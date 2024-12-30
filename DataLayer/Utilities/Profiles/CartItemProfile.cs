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
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.bookId))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.book != null ? src.book.Title : string.Empty))
            .ForMember(dest => dest.BookPrice, opt => opt.MapFrom(src => src.book != null ? src.book.price : 0))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Price));

        }
    
    }
}
