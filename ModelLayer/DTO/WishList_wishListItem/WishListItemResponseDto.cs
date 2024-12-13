using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.WishList_wishListItem
{
    public class WishListItemResponseDto
    {
        public int Id { get; set; }

        public int bookId { get; set; }

        public int WishListId { get; set; }
    }
}
