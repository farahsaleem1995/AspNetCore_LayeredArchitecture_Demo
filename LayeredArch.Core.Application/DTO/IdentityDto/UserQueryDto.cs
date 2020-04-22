using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.DTO.IdentityDto
{
    public class UserQueryDto : IQueryObject
    {
        public string RoleId { get; set; }

        public bool? IsActive { get; set; }

        public string SortBy { get; set; }

        public bool? IsSortAscending { get; set; }

        public int Page { get; set; }

        public byte PageSize { get; set; }
    }
}
