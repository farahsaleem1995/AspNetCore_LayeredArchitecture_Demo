using LayeredArch.Core.Domain.Interfaces;
using LayeredArch.Core.Domain.Models.Auth;
using LayeredArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Infra.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;
        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> Get(int refreshTokenId, string accessToken)
        {
            return await _context.RefreshTokens
                          .Include(rt => rt.User)
                          .Where(rt => rt.Id == refreshTokenId)
                          .Where(rt => rt.AccessToken == accessToken)
                          .FirstOrDefaultAsync();
        }

        public void Add(RefreshToken refreshToken)
        {
            _context.Add(refreshToken);
        }

        public async Task RemoveByUserIdAsync(string userId)
        {
            var refreshTokens = await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
            foreach (var refreshToken in refreshTokens)
            {
                _context.Remove(refreshToken);
            }
        }
    }
}
