using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Account
{
    public class AssignRoleResource
    {
        [Required]
        public string RoleName { get; set; }
    }
}
