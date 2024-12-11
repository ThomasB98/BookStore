using ModelLayer.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public Role Role { get; set; }

        public string Address { get; set; } = "";

        public bool HasWishList { get; set; }
    }
}
