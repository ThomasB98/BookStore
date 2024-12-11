using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model.Entity
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public string? Publisher { get; set; }

        [Required]
        public string? descrption { get; set; }

        [Required]
        public int stock { get; set; }

        [Required]
        [Url(ErrorMessage = "The Image must be a valid URL.")]
        public string? img { get; set; }

        public ICollection<CartItem>? cartItems { get; set; }

        public ICollection<wishListItem>? wishListItems { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
