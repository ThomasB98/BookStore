using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model.Enums;

namespace ModelLayer.DTO.Order
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public DateTime orderDate { get; set; }
        public OrderStatus orderStatus { get; set; }
        public float totalAmount { get; set; }
        public ICollection<OrderItemResponseDto> Items { get; set; }
    }
}
