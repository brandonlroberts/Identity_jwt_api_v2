using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Identity_JWT_API.Extensions.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public ICollection<AppUserRole> UserRoles { get; set; }
        public string FullName { get; private set; }
    }
}