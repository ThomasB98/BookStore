using AutoMapper;
using ModelLayer.DTO.Shipping;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Profiles
{
    public class ShippingProfile : Profile
    {
        public ShippingProfile() { 
            CreateMap<Shipping,ShippingCreateDto>();
            CreateMap<ShippingCreateDto, Shipping>();

            CreateMap<Shipping, ShippingUpdateDto>();
            CreateMap<ShippingUpdateDto, Shipping>();

        }
    }
}
