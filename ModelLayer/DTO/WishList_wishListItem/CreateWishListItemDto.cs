using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.WishList_wishListItem
{
    public class CreateWishListItemDto
    {
        public int WishListId { get; set; }
        public int BookId { get; set; }
    }
}
