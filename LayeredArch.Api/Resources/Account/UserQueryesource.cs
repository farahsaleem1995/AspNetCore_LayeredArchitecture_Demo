using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Account
{
    public class UserQueryResource
    {
        public string RoleId { get; set; }

        public bool? IsActive { get; set; }

        public string SortBy { get; set; }

        public bool? IsSortAscending { get; set; }

        public int Page { get; set; }

        public byte PageSize { get; set; }
    }
}
