using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<UserDto> CheckUser(RefreshTokenDto token);
        Task<RefreshTokenDto> GetNew(string userId, string accessToken);
    }
}
