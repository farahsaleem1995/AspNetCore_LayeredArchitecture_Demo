using LayeredArch.Core.Application.Resources.AuthDto;
using LayeredArch.Core.Application.Resources.IdentityDto;
using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> FindUserByIdAsync(string userId);

        Task<IEnumerable<string>> GetRolesAsync(UserDto user);

        Task<RoleDto> FindRoleByNameAsync(string roleName);

        Task<UserDto> CreateUserAsync(RegisterUserDto user, RoleDto role);
    }
}
