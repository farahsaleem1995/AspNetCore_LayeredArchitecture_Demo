using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.Resources.IdentityDto
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email{ get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<RoleDto> Roles { get; set; }
    }
}
