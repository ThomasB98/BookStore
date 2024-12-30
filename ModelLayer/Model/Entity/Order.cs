using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model.Enums;

namespace ModelLayer.Model.Entity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User? User { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime orderDate { get; set; }

        [Required]
        public OrderStatus orderStatus { get; set; }

        [Required]
        public float totalAmount { get; set; }


        public ICollection<OrderItem> Items { get; set; }

        public Shipping Shipping { get; set; }

    }
}
