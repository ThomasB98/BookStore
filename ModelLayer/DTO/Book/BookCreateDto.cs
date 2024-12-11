using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Book
{
    public class BookCreateDto
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string? Publisher { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        [Url(ErrorMessage = "The Image must be a valid URL.")]
        public string? Img { get; set; }
    }
}
