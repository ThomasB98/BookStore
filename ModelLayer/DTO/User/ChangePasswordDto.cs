using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.User
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = "";

        [Required(ErrorMessage = "New password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "New password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = "";

        [Compare("NewPassword", ErrorMessage = "New passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; } = "";
    }
}
