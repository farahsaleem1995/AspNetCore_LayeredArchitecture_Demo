using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LayeredArch.Core.Domain.Models.Auth
{
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public DomainUser User { get; set; }

        [Required]
        public string AccessToken { get; set; }

        public DateTime CreatedAt { get; set; }

        public RefreshToken()
        {
            this.CreatedAt = DateTime.Now;
        }
    }
}
