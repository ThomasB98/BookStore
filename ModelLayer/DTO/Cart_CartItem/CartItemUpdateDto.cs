using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Cart_CartItem
{
    public class CartItemUpdateDto
    {
        [Required]
        public int CartItemId { get; set; }

        [Required]
        public int NewQuantity { get; set; }
    }
}
