using Microsoft.AspNetCore.Identity;
using MYT.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data.AppContext
{
    public class DbUser : IdentityUser
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
