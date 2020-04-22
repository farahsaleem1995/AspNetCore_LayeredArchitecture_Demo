using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Account
{
    public class UserAdminstrationResource : UserResource
    {
        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }
    }
}
