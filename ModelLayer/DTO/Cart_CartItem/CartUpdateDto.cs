using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Cart_CartItem
{
    public class CartUpdateDto
    {
        [Required]
        public float Total { get; set; }
        public ICollection<CartItemUpdateDto>? CartItems { get; set; }
    }
}
