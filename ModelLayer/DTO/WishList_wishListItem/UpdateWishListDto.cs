using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.WishList_wishListItem
{
    public class UpdateWishListDto
    {
        public ICollection<CreateWishListItemDto>? WishListItems { get; set; }
    }
}
