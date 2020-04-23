using LayeredArch.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayeredArch.Infra.Data.Helpers
{
    public class UserResult : IUserResult
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }
    }
}
