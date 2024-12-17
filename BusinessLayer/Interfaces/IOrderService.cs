using DataLayer.Utilities.ResponseBody;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseBody<Order>> PlaceOrder(int ShippingId);

        Task<ResponseBody<Order>> GetOrderByIdAsync(int orderId);

        Task<ResponseBody<IEnumerable<Order>>> GetOrdersByUserIdAsync(int userId);

        Task<ResponseBody<bool>> DeleteOrderAsync(int orderId);
    }
}
