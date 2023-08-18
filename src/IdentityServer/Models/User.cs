using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}