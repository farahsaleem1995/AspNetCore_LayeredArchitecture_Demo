using LayeredArch.Core.Domain.Interfaces;
using LayeredArch.Core.Domain.Models;
using LayeredArch.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<DomainUser> _userManager;
        private readonly RoleManager<DomainRole> _roleManager;
        private readonly SignInManager<DomainUser> _signInManager;

        public UserRepository(UserManager<DomainUser> userManager,
                                RoleManager<DomainRole> roleManager,
                                SignInManager<DomainUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<DomainUser>> GetUsersAsync(bool isAdmin = false)
        {
            var query = _userManager.Users
                            .Include(u => u.UserRoles)
                                .ThenInclude(ur => ur.Role)
                            .AsQueryable();
            if (!isAdmin)
            {
                query = query.Where(u => !u.IsActive).AsQueryable();
            }
            return await query.ToListAsync();
        }

        public async Task<DomainUser> FindByIdAsync(string userId, bool isAdmin = false)
        {
            var query =  _userManager.Users
                            .Include(u => u.UserRoles)
                                .ThenInclude(ur => ur.Role)
                            .Where(u => u.Id == userId)
                            .AsQueryable();
            if (!isAdmin)
            {
                query = query.Where(u => !u.IsActive).AsQueryable();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<DomainUser> FindByNameAsync(string userName, bool isAdmin = false)
        {
            var query =  _userManager.Users
                            .Include(u => u.UserRoles)
                                .ThenInclude(ur => ur.Role)
                            .Where(u => u.UserName == userName)
                            .AsQueryable();
            if (!isAdmin)
            {
                query = query.Where(u => !u.IsActive).AsQueryable();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<DomainUser> FindByConfirmedPhoneNumberAsync(string phoneNumber)
        {
            return await _userManager.Users
                            .Where(u => u.PhoneNumberConfirmed)
                            .Where(u => u.PhoneNumber == phoneNumber)
                            .FirstOrDefaultAsync();
        }

        public async Task<DomainUser> FindByConfirmedEmailAsync(string email)
        {
            return await _userManager.Users
                            .Where(u => u.EmailConfirmed)
                            .Where(u => u.Email == email)
                            .FirstOrDefaultAsync();
        }
    }
}
