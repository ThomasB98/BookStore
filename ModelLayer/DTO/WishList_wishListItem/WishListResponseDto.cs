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
    public class WishListResponseDto
    {
        public int id { get; set; }

        public int userId { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<wishListItem>? WishListItems { get; set; }
    }
}
