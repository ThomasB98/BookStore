using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Shipping
{
    public class ShippingCreateDto
    {
        [Required(ErrorMessage = "Address Line 1 is required")]
        [StringLength(100, ErrorMessage = "Address Line 1 cannot exceed 100 characters")]
        public string AddressLine1 { get; set; }

        [StringLength(100, ErrorMessage = "Address Line 2 cannot exceed 100 characters")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [StringLength(20, ErrorMessage = "Postal Code cannot exceed 20 characters")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        public int? OrderId { get; set; }
    }
}
