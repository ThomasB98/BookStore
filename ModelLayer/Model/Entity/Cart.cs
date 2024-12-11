using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model.Entity
{
    [Table("cart")]
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User? user {  get; set; }

        [Required]
        public float total { get; set; }

        public ICollection<CartItem> cartItems { get; set; }
    }
}
