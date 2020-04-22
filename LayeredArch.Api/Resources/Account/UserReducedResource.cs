using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Account
{
    public class UserReducedResource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<RoleResource> UserRoles { get; set; }
    }
}
