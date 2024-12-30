using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Order;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.service
{
    public class OrderBL : IOrderService
    {

        private readonly IOrder _orderRepo;

        public OrderBL(IOrder orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public Task<ResponseBody<bool>> DeleteOrderAsync(int orderId)
        {
            return _orderRepo.DeleteOrderAsync(orderId);
        }

        public Task<ResponseBody<Order>> GetOrderByIdAsync(int orderId)
        {
            return _orderRepo.GetOrderByIdAsync(orderId);
        }

        public Task<ResponseBody<IEnumerable<OrderResponseDto>>> GetOrdersByUserIdAsync(int userId)
        {
            return _orderRepo.GetOrdersByUserIdAsync(userId);
        }

        public Task<ResponseBody<Order>> PlaceOrder(int ShippingId)
        {
            return _orderRepo.PlaceOrder(ShippingId);
        }
    }
}
