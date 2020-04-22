using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Auth
{
    public class RefreshTokenResource
    {
        [Required]
        public int RefreshToken { get; set; }

        [Required]
        public string AccessToken { get; set; }
    }
}
