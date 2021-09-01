using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Identity_JWT_API.Extensions.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}