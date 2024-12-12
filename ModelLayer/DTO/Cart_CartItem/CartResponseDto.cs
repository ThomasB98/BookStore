using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Cart_CartItem
{
    public class CartResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Total { get; set; }
        public ICollection<CartItemResponseDto>? CartItems { get; set; }
    }
}
