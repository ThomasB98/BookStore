using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model.Entity
{
    [Table("wishlistitem")]
    public class wishListItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int WishListId {  get; set; }

        [ForeignKey("WishListId")]
        public WishList? WishList { get; set; }

        [Required]
        public int bookId { get; set; }

        [ForeignKey("bookId")]
        public Book? book { get; set; }
    }
}
