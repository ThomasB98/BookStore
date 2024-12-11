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
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = false;

        [Required]
        public Role role { get; set; } = Role.USER;

        public string address { get; set; } = "";

        public ICollection<Order>? orders { get; set; }

        public WishList? WishList { get; set; }
    }
}
