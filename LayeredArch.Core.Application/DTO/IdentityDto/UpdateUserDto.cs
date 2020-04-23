using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LayeredArch.Core.Application.DTO.IdentityDto
{
    public class UpdateUserDto
    {
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }
    }
}
