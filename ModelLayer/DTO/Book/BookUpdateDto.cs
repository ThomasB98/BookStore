using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Book
{
    public class BookUpdateDto
    {
        [Required]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public float? Price { get; set; }

        public string? Publisher { get; set; }

        public string? Description { get; set; }

        public int? Stock { get; set; }

        [Url(ErrorMessage = "The Image must be a valid URL.")]
        public string? Img { get; set; }
    }
}
