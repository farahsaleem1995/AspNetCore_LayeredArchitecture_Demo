using LayeredArch.Core.Application.DTO;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<QueryResultDto<UserDto>> GetUsersAsync(UserQueryDto queryDto);
        Task<UserDto> FindByIdAsync(string userId, bool isAdmin = false);
        Task<UserDto> FindByNameAsync(string userId, bool isAdmin = false);
        Task<QueryResultDto<UserDto>> SearchByNameAsync(string searchKey, UserQueryDto queryDto, bool isAdmin = false);
        Task<IEnumerable<string>> GetRolesAsync(string userId);
        Task<RoleDto> FindRoleByNameAsync(string roleName);
        Task<UserDto> CreateUserAsync(RegisterUserDto user, RoleDto role, string password);
        Task UpdateUserAsync(string userId, UpdateUserDto userDto, bool isAdmin = false);
        Task DeleteUserAsync(string userId);
        Task ActivateUserAsync(string userId);
        Task<UserDto> CheckPasswordSignInAsync(string userName, string password);
        Task<string> GetPhoneNumberTokenAsync(string userId, string phoneNumber);
        Task<string> GetPasswordResetTokenAsync(string userName);
        Task<string> GetEmailConfirmationTokenAsync(string userId);
        Task ChangePhoneNumberAsync(string userId, string securityCode, string newPhoneNumber = null);
        Task ResetPasswordAsync(string userName, string securityCode, string password);
        Task ConfirmEmailAsync(string userId, string token);
    }
}
