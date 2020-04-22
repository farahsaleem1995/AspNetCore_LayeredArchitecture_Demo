using LayeredArch.Core.Application.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Core.Application.DTO.AuthDto
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public UserDto User { get; set; }

        public string AccessToken { get; set; }
    }
}
