using LayeredArch.Core.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> Get(int refreshTokenId, string accessToken);
        void Add(RefreshToken refreshToken);
        Task RemoveByUserIdAsync(string userId);
    }
}
