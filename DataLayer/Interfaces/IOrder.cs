using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Cart_CartItem;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IOrder
    {
        Task<ResponseBody<Order>> PlaceOrder(int ShippingId);

        Task<ResponseBody<Order>> GetOrderByIdAsync(int orderId);

        Task<ResponseBody<IEnumerable<Order>>> GetOrdersByUserIdAsync(int userId);

        Task<ResponseBody<bool>> DeleteOrderAsync(int orderId);
    }
}
