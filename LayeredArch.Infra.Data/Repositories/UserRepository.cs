using LayeredArch.Core.Domain.Interfaces;
using LayeredArch.Core.Domain.Models;
using LayeredArch.Core.Domain.Models.Identity;
using LayeredArch.Infra.Data.Helpers;
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
                query = query.Where(u => u.IsActive).AsQueryable();
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
                query = query.Where(u => u.IsActive).AsQueryable();
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
                query = query.Where(u => u.IsActive).AsQueryable();
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

        public async Task<IEnumerable<DomainRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRolesAsync(DomainUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<DomainRole> FindRoleById(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<IUserResult> AddToRoleAsync(DomainUser user, string roleName)
        {
            var userResult = new UserResult();

            var result = await _userManager.AddToRoleAsync(user, roleName);
            userResult.Succeeded = result.Succeeded;
            userResult.Error = result.Errors.Select(e => e.Description).FirstOrDefault();

            return userResult;
        }

        public async Task<IUserResult> RemoveFromRoleAsync(DomainUser user, string roleName)
        {
            var userResult = new UserResult();

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            userResult.Succeeded = result.Succeeded;
            userResult.Error = result.Errors.Select(e => e.Description).FirstOrDefault();

            return userResult;
        }

        public async Task<DomainRole> FindRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<IUserResult> CreateAsync(DomainUser user, string password)
        {
            var userResult = new UserResult();

            var result = await _userManager.CreateAsync(user, password);
            userResult.Succeeded = result.Succeeded;
            userResult.Error = result.Errors.Select(e => e.Description).FirstOrDefault();

            return userResult;
        }

        public async Task<ILogInResult> CheckPasswordSignInAsync(DomainUser user, string password)
        {
            var logInResult = new LogInResult();

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
            logInResult.Succeeded = result.Succeeded;
            logInResult.IsLockedOut = result.IsLockedOut;
            logInResult.IsNotAllowed = result.IsNotAllowed;

            return logInResult;
        }

        public async Task<bool> CheckPasswordAsync(DomainUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> CanSignInAsync(DomainUser user)
        {
            return await _signInManager.CanSignInAsync(user);
        }

        public async Task<string> GenerateChangePhoneNumberTokenAsync(DomainUser user, string phoneNumber)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(DomainUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(DomainUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IUserResult> ChangePhoneNumberAsync(DomainUser user, string phoneNumber, string securityCode)
        {
            var userResult = new UserResult();

            var result = await _userManager.ChangePhoneNumberAsync(user, phoneNumber, securityCode);
            userResult.Succeeded = result.Succeeded;
            userResult.Error = result.Errors.Select(e => e.Description).FirstOrDefault();

            return userResult;
        }

        public async Task<IUserResult> ResetPasswordAsync(DomainUser user, string securityCode, string password)
        {
            var userResult = new UserResult();

            var result = await _userManager.ResetPasswordAsync(user, securityCode, password);
            userResult.Succeeded = result.Succeeded;
            userResult.Error = result.Errors.Select(e => e.Description).FirstOrDefault();

            return userResult;
        }

        public async Task<IUserResult> ConfirmEmailAsync(DomainUser user, string token)
        {
            var userResult = new UserResult();

            var result = await _userManager.ConfirmEmailAsync(user, token);
            userResult.Succeeded = result.Succeeded;
            userResult.Error = result.Errors.Select(e => e.Description).FirstOrDefault();

            return userResult;
        }

        public async Task<bool> IsLockedOutAsync(DomainUser user)
        {
            return await _userManager.IsLockedOutAsync(user);
        }

        public async Task SetLockoutEndDateAsync(DomainUser user, DateTimeOffset? lockoutEnd)
        {
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
        }

    }
}
