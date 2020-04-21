using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> FindByIdAsync(string userId);

        Task<UserDto> FindByNameAsync(string userId);

        Task<IEnumerable<string>> GetRolesAsync(string userId);

        Task<RoleDto> FindRoleByNameAsync(string roleName);

        Task<UserDto> CreateUserAsync(UserDto user, RoleDto role, string password);

        Task UpdateUserAsync(string userId, UserDto userDto);

        Task<UserDto> CheckPasswordSignInAsync(string userName, string password);

        Task<string> GetPhoneNumberTokenAsync(string userId, string phoneNumber);

        Task<string> GetPasswordResetTokenAsync(string userName);

        Task<string> GetEmailConfirmationTokenAsync(string userId);

        Task ChangePhoneNumberAsync(string userId, string securityCode, string newPhoneNumber = null);

        Task ResetPasswordAsync(string userName, string securityCode, string password);

        Task ConfirmEmailAsync(string userId, string token);
    }
}
