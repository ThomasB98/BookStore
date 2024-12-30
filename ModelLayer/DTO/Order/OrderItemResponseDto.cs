using ModelLayer.DTO.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Order
{
    public class OrderItemResponseDto
    {
        public int Id { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
        public BookResponseDto Book { get; set; }
    }
}
