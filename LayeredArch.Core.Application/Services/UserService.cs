﻿using AutoMapper;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using LayeredArch.Core.Application.Exceptions;
using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Domain.Interfaces;
using LayeredArch.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<DomainUser> _userManager;
        private readonly RoleManager<DomainRole> _roleManager;
        private readonly SignInManager<DomainUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UserManager<DomainUser> userManager, 
                            RoleManager<DomainRole> roleManager,
                            SignInManager<DomainUser> signInManager,
                            IUnitOfWork unitOfWork,
                            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> FindByIdAsync(string userId)
        {
            var user = await _userManager.Users
                                .Include(u => u.UserRoles)
                                    .ThenInclude(ur => ur.Role)
                                .Where(u => u.Id == userId)
                                .FirstOrDefaultAsync();

            return _mapper.Map<DomainUser, UserDto>(user);
        }

        public async Task<UserDto> FindByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return _mapper.Map<DomainUser, UserDto>(user);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(string userId)
        {
            var domainUser = await _userManager.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }
            var roles = await _userManager.GetRolesAsync(domainUser);

            return roles;
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

        public async Task<UserDto> CreateUserAsync(UserDto user, RoleDto role, string password)
        {
            var domainUser = _mapper.Map<UserDto, DomainUser>(user);
            var domainRole = _mapper.Map<RoleDto, DomainRole>(role);
            domainUser.UserRoles.Add(new DomainUserRole() { RoleId = domainRole.Id });

            var result = await _userManager.CreateAsync(domainUser, password);
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();
                throw new CoreException(400, error.Description);
            }

            return _mapper.Map<DomainUser, UserDto>(domainUser);
        }

        public async Task UpdateUserAsync(string userId, UserDto userDto)
        {
            var updateUser = await _userManager.FindByIdAsync(userId);
            if (updateUser == null)
            {
                throw new CoreException(404, "User not found");
            }
            _mapper.Map<UserDto, DomainUser>(userDto, updateUser);
        }

        public async Task<UserDto> CheckPasswordSignInAsync(string userName, string password)
        {
            var domainUser = await _userManager.FindByNameAsync(userName);
            if (domainUser != null && !domainUser.PhoneNumberConfirmed && await _userManager.CheckPasswordAsync(domainUser, password))
            {
                throw new CoreException(404, "Phone number not confirmed yet");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(domainUser, password, false);
            if (!result.Succeeded)
            {
                throw new CoreException(400, "Username and password does not match");
            }

            return _mapper.Map<DomainUser, UserDto>(domainUser);
        }

        public async Task<string> GetPhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            var domainUser = await _userManager.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            return await _userManager.GenerateChangePhoneNumberTokenAsync(domainUser, phoneNumber);
        }

        public async Task<string> GetPasswordResetTokenAsync(string userName)
        {
            var domainUser = await _userManager.FindByNameAsync(userName);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            return await _userManager.GeneratePasswordResetTokenAsync(domainUser);
        }

        public async Task<string> GetEmailConfirmationTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new CoreException(404, "User not found");
            }
            if (user.EmailConfirmed)
            {
                throw new CoreException(400, "Email is already confirmed!");
            }
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task ChangePhoneNumberAsync(string userId, string securityCode, string newPhoneNumber = null)
        {
            var domainUser = await _userManager.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            var phoneNumber = newPhoneNumber ?? domainUser.PhoneNumber;
            var previousUser = await _userManager.Users
                                        .Where(u => u.PhoneNumberConfirmed)
                                        .Where(u => u.PhoneNumber == phoneNumber)
                                        .FirstOrDefaultAsync();

            var result = await _userManager.ChangePhoneNumberAsync(domainUser, phoneNumber, securityCode);
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();
                throw new CoreException(400, error.Description);
            }

            if (previousUser != null)
            {
                previousUser.PhoneNumberConfirmed = false;
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task ResetPasswordAsync(string userName, string securityCode, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var result = await _userManager.ResetPasswordAsync(user, securityCode, password);
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();
                throw new CoreException(400, error.Description);
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            }
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new CoreException(404, "User not found");
            }

            var previousUser = await _userManager.Users
                                        .Where(u => u.EmailConfirmed)
                                        .Where(u => u.Email == user.Email)
                                        .FirstOrDefaultAsync();
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();
                throw new CoreException(400, error.Description);
            }
            if (previousUser != null)
            {
                previousUser.EmailConfirmed = false;
                await _unitOfWork.CompleteAsync();
            }
        }

    }
}
