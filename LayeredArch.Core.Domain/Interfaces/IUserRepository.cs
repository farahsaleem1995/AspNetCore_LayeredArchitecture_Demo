using LayeredArch.Core.Domain.Models;
using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<DomainUser>> GetUsersAsync(bool isAdmin = false);
        Task<DomainUser> FindByIdAsync(string userId, bool isAdmin = false);
        Task<DomainUser> FindByNameAsync(string userId, bool isAdmin = false);
        Task<DomainUser> FindByConfirmedPhoneNumberAsync(string userId);
        Task<DomainUser> FindByConfirmedEmailAsync(string userId);
        Task<IEnumerable<DomainRole>> GetRolesAsync();
        Task<IEnumerable<string>> GetRolesAsync(DomainUser user);
        Task<DomainRole> FindRoleById(string roleId);
        Task<IUserResult> AddToRoleAsync(DomainUser user, string roleName);
        Task<IUserResult> RemoveFromRoleAsync(DomainUser user, string roleName);
        Task<DomainRole> FindRoleByNameAsync(string roleName);
        Task<IUserResult> CreateAsync(DomainUser user, string password);
        Task<ILogInResult> CheckPasswordSignInAsync(DomainUser user, string password);
        Task<bool> CheckPasswordAsync(DomainUser user, string password);
        Task<bool> CanSignInAsync(DomainUser user);
        Task<string> GenerateChangePhoneNumberTokenAsync(DomainUser user, string phoneNumber);
        Task<string> GeneratePasswordResetTokenAsync(DomainUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(DomainUser user);
        Task<IUserResult> ChangePhoneNumberAsync(DomainUser user, string phoneNumber, string securityCode);
        Task<IUserResult> ResetPasswordAsync(DomainUser user, string securityCode, string password);
        Task<IUserResult> ConfirmEmailAsync(DomainUser user, string token);
        Task<bool> IsLockedOutAsync(DomainUser user);
        Task SetLockoutEndDateAsync(DomainUser user, DateTimeOffset? lockoutEnd);
    }
}
