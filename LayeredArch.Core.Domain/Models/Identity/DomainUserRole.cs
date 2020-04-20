using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Domain.Models.Identity
{
    public class DomainUserRole : IdentityUserRole<string>
    {
        public virtual DomainUser User { get; set; }
        public virtual DomainRole Role { get; set; }
    }
}
