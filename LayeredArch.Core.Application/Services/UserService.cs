using AutoMapper;
using LayeredArch.Core.Application.Exceptions;
using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Application.Resources.AuthDto;
using LayeredArch.Core.Application.Resources.IdentityDto;
using LayeredArch.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<DomainUser> _userManager;
        private readonly RoleManager<DomainRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<DomainUser> userManager, 
                            RoleManager<DomainRole> roleManager,
                            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserDto> FindUserByIdAsync(string userId)
        {
            var user = await _userManager.Users
                                .Include(u => u.UserRoles)
                                .Where(u => u.Id == userId)
                                .FirstOrDefaultAsync();

            return _mapper.Map<DomainUser, UserDto>(user);
        }

        public async Task<RoleDto> FindRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new CoreException(404, "Role not found");
            }

            return _mapper.Map<DomainRole, RoleDto>(role);
        }

        public async Task<UserDto> CreateUserAsync(RegisterUserDto user, RoleDto role)
        {
            var domainUser = _mapper.Map<RegisterUserDto, DomainUser>(user);
            var domainRole = _mapper.Map<RoleDto, DomainRole>(role);


            var result = await _userManager.CreateAsync(domainUser, user.Password);
            if (result.Succeeded)
            {
                var domainUserRole = new DomainUserRole() { Role = domainRole };
                domainUser.UserRoles.Add(domainUserRole);
                return _mapper.Map<DomainUser, UserDto>(domainUser);
            }
            else
            {
                var error = result.Errors.FirstOrDefault();
                throw new CoreException(400, error.Description);
            }
        }

        public async Task<IEnumerable<string>> GetRolesAsync(UserDto user)
        {
            var domainUser = _mapper.Map<UserDto, DomainUser>(user);
            var roles = await _userManager.GetRolesAsync(domainUser);

            return roles;
        }
    }
}
