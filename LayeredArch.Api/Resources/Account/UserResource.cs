using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Account
{
    public class UserResource : UserReducedResource
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
