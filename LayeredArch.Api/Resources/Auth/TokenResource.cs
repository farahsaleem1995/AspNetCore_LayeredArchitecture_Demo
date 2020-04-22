using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Resources.Auth
{
    public class TokenResource
    {
        public int RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
