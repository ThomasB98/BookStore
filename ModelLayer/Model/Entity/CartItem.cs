using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model.Entity
{
    [Table("cartitem")]
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int cartId { get; set; }

        [ForeignKey("cartId")]
        public Cart? Cart { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int bookId { get; set; }

        [ForeignKey("bookId")]
        public Book? book { get; set; }
    }
}
