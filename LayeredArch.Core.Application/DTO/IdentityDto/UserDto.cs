using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.DTO.IdentityDto
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email{ get; set; }

        public string NormalizedEmail { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string PhoneNumber { get; set; }

        public string TwoFactorEnabled { get; set; }

        public string LockoutEnd { get; set; }

        public string LockoutEnabled { get; set; }

        public string AccessFailedCount { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<RoleDto> Roles { get; set; }
    }
}
