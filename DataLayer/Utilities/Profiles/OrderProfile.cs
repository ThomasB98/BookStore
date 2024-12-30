using AutoMapper;
using ModelLayer.DTO.Order;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderResponseDto, Order>();
        }
    }
}
