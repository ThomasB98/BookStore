using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO.Book;

namespace ModelLayer.DTO.WishList_wishListItem
{
    public class WishListResponseDto
    {
        public int id { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<BookResponseDto>? Books { get; set; }
    }
}
