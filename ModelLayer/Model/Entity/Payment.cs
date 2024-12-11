using ModelLayer.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model.Entity
{
    [Table("payment")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int orderID { get; set; }

        [ForeignKey("orderId")]
        public Order? Order { get; set; }

        [Required]
        public decimal amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

    }
}
