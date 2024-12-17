using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Shipping
{
    public class ShippingUpdateDto
    {
        [Required(ErrorMessage = "Shipping ID is required")]
        public int ShippingId { get; set; }

        [StringLength(100, ErrorMessage = "Address Line 1 cannot exceed 100 characters")]
        public string? AddressLine1 { get; set; }

        [StringLength(100, ErrorMessage = "Address Line 2 cannot exceed 100 characters")]
        public string? AddressLine2 { get; set; }

        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string? State { get; set; }

        [StringLength(20, ErrorMessage = "Postal Code cannot exceed 20 characters")]
        public string? PostalCode { get; set; }

        public int? UserId { get; set; }

        public int? OrderId { get; set; }
    }
}
