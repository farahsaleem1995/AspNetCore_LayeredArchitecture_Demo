using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Models.Identity
{
    public class DomainRole : IdentityRole
    {
        public virtual ICollection<DomainUserRole> UserRoles { get; set; }

        public DomainRole()
        {
            this.UserRoles = new List<DomainUserRole>();
        }
    }
}
