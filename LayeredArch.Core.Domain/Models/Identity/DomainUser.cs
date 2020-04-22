using LayeredArch.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LayeredArch.Core.Domain.Models.Identity
{
    public class DomainUser : IdentityUser, ISearchable
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<DomainUserRole> UserRoles { get; set; }

        [NotMapped]
        public string SeacrhKey
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        [NotMapped]
        public int Similarity { get; set; }

        public DomainUser()
        {
            this.IsActive = true;
            this.CreatedAt = DateTime.Now;

            this.UserRoles = new List<DomainUserRole>();
        }
    }
}
