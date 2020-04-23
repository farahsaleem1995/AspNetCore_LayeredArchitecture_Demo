using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Auth
{
    public class RegisterUserResource
    {
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
