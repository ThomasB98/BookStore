using Microsoft.AspNetCore.Authorization;
using ModelLayer.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Role _role { get; }

        public PermissionRequirement(Role role)
        {
            _role = role;
        }

    }
}
