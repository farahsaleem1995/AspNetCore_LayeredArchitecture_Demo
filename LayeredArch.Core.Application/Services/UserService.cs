using AutoMapper;
using LayeredArch.Core.Application.DTO;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using LayeredArch.Core.Application.Exceptions;
using LayeredArch.Core.Application.Extensions;
using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Domain.Interfaces;
using LayeredArch.Core.Domain.Models;
using LayeredArch.Core.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
                            IUnitOfWork unitOfWork,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<QueryResultDto<UserDto>> GetUsersAsync(UserQueryDto queryDto)
        {
            var result = new QueryResult<DomainUser>();
            var users = await _userRepository.GetUsersAsync();

            var query = users.AsQueryable();
            var filterColumnsMap = new Dictionary<string, Expression<Func<DomainUser, bool>>>()
            {
                ["IsActive"] = u => u.IsActive == queryDto.IsActive,
                ["RoleId"] = u => u.UserRoles.Select(ur => ur.RoleId).Contains(queryDto.RoleId)
            };
            query = query.ApplyFiltering(queryDto, filterColumnsMap);

            var orderoColumnsMap = new Dictionary<string, Expression<Func<DomainUser, object>>>()
            {
                ["fisrtName"] = u => u.FirstName,
                ["lastName"] = u => u.LastName,
                ["email"] = u => u.Email,
                ["createdAt"] = u => u.CreatedAt
            };
            query = query.ApplyOrdering(queryDto, orderoColumnsMap);

            result.TotalItems = query.Count();
            query = query.ApplyPaging(queryDto);

            result.Items = query.ToList();
            return _mapper.Map<QueryResult<DomainUser>, QueryResultDto<UserDto>>(result);
        }

        public async Task<UserDto> FindByIdAsync(string userId, bool isAdmin = false)
        {
            var user = await _userRepository.FindByIdAsync(userId, isAdmin: isAdmin);

            return _mapper.Map<DomainUser, UserDto>(user);
        }

        public async Task<UserDto> FindByNameAsync(string userName, bool isAdmin = false)
        {
            var user = await _userRepository.FindByNameAsync(userName, isAdmin: isAdmin);
            return _mapper.Map<DomainUser, UserDto>(user);
        }

        public async Task<QueryResultDto<UserDto>> SearchByNameAsync(string searchKey, UserQueryDto queryDto, bool isAdmin = false)
        {
            var result = new QueryResult<DomainUser>();
            var users = await _userRepository.GetUsersAsync(isAdmin: isAdmin);
            if (!isAdmin)
            {
                 users = users.Where(u => u.IsActive == true).AsQueryable();
            }

            var query = users.AsQueryable();
            query = query.ApplySearch(searchKey);
            result.TotalItems = query.Count();

            query = query.ApplyPaging(queryDto);

            var items = query.ToList();
            result.Items = items.OrderBy(q => q.Similarity);

            return _mapper.Map<QueryResult<DomainUser>, QueryResultDto<UserDto>>(result);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _userRepository.GetRolesAsync();
            return _mapper.Map<IEnumerable<DomainRole>, IEnumerable<RoleDto>>(roles);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(string userId)
        {
            var domainUser = await _userRepository.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }
            var roles = await _userRepository.GetRolesAsync(domainUser);

            return roles;
        }

        public async Task<RoleDto> FindRoleById(string roleId)
        {
            var domainRole = await _userRepository.FindRoleById(roleId);
            return _mapper.Map<DomainRole, RoleDto>(domainRole);
        }

        public async Task<RoleDto> FindRoleByNameAsync(string roleName)
        {
            var role = await _userRepository.FindRoleByNameAsync(roleName);
            if (role == null)
            {
                throw new CoreException(404, "Role not found");
            }

            return _mapper.Map<DomainRole, RoleDto>(role);
        }

        public async Task AddToRoleAsync(string userId, string roleName)
        {
            var domainUser = await _userRepository.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            var result = await _userRepository.AddToRoleAsync(domainUser, roleName);
            if (!result.Succeeded)
            {
                throw new CoreException(400, result.Error);
            }
        }

        public async Task RemoveFromRoleAsync(string userId, string roleName)
        {
            var domainUser = await _userRepository.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            var result = await _userRepository.RemoveFromRoleAsync(domainUser, roleName);
            if (!result.Succeeded)
            {
                throw new CoreException(400, result.Error);
            }
        }

        public async Task<UserDto> CreateUserAsync(RegisterUserDto user, RoleDto role, string password)
        {
            var domainUser = _mapper.Map<RegisterUserDto, DomainUser>(user);
            var domainRole = _mapper.Map<RoleDto, DomainRole>(role);
            domainUser.UserRoles.Add(new DomainUserRole() { RoleId = domainRole.Id });

            var result = await _userRepository.CreateAsync(domainUser, password);
            if (!result.Succeeded)
            {
                throw new CoreException(400, result.Error);
            }

            return _mapper.Map<DomainUser, UserDto>(domainUser);
        }

        public async Task UpdateUserAsync(string userId, UpdateUserDto updateUserDto, bool isAdmin = false)
        {
            var updateUser = await _userRepository.FindByIdAsync(userId, isAdmin: isAdmin);
            if (updateUser == null)
            {
                throw new CoreException(404, "User not found");
            }
            _mapper.Map(updateUserDto, updateUser);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var updateUser = await _userRepository.FindByIdAsync(userId, isAdmin: true);
            if (updateUser == null)
            {
                throw new CoreException(404, "User not found");
            }
            updateUser.IsActive = false;
            await _unitOfWork.CompleteAsync();
        }

        public async Task ActivateUserAsync(string userId)
        {
            var updateUser = await _userRepository.FindByIdAsync(userId, isAdmin: true);
            if (updateUser == null)
            {
                throw new CoreException(404, "User not found");
            }
            updateUser.IsActive = true;
            await _unitOfWork.CompleteAsync();
        }

        public async Task<UserDto> CheckPasswordSignInAsync(string userName, string password)
        {
            var domainUser = await _userRepository.FindByNameAsync(userName);
            if (domainUser != null && !domainUser.PhoneNumberConfirmed && await _userRepository.CheckPasswordAsync(domainUser, password))
            {
                throw new CoreException(404, "Phone number not confirmed yet");
            }
            var result = await _userRepository.CheckPasswordSignInAsync(domainUser, password);
            if (result.IsLockedOut)
            {
                throw new CoreException(403, "Invalid Request");
            }
            if (!result.Succeeded)
            {
                throw new CoreException(400, "Username and password does not match");
            }

            return _mapper.Map<DomainUser, UserDto>(domainUser);
        }

        public async Task<string> GetPhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            var domainUser = await _userRepository.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            return await _userRepository.GenerateChangePhoneNumberTokenAsync(domainUser, phoneNumber);
        }

        public async Task<string> GetPasswordResetTokenAsync(string userName)
        {
            var domainUser = await _userRepository.FindByNameAsync(userName);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            return await _userRepository.GeneratePasswordResetTokenAsync(domainUser);
        }

        public async Task<string> GetEmailConfirmationTokenAsync(string userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                throw new CoreException(404, "User not found");
            }
            if (user.EmailConfirmed)
            {
                throw new CoreException(400, "Email is already confirmed!");
            }
            return await _userRepository.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task ChangePhoneNumberAsync(string userId, string securityCode, string newPhoneNumber = null)
        {
            var domainUser = await _userRepository.FindByIdAsync(userId);
            if (domainUser == null)
            {
                throw new CoreException(404, "User not found");
            }

            var phoneNumber = newPhoneNumber ?? domainUser.PhoneNumber;
            var previousUser = await _userRepository.FindByConfirmedPhoneNumberAsync(domainUser.Id);

            var result = await _userRepository.ChangePhoneNumberAsync(domainUser, phoneNumber, securityCode);
            if (!result.Succeeded)
            {
                throw new CoreException(400, result.Error);
            }

            if (previousUser != null)
            {
                previousUser.PhoneNumberConfirmed = false;
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task ResetPasswordAsync(string userName, string securityCode, string password)
        {
            var user = await _userRepository.FindByNameAsync(userName);

            var result = await _userRepository.ResetPasswordAsync(user, securityCode, password);
            if (!result.Succeeded)
            {
                throw new CoreException(400, result.Error);
            }
            if (await _userRepository.IsLockedOutAsync(user))
            {
                await _userRepository.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            }
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                throw new CoreException(404, "User not found");
            }

            var previousUser = await _userRepository.FindByConfirmedEmailAsync(user.Id);
            var result = await _userRepository.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new CoreException(400, result.Error);
            }
            if (previousUser != null)
            {
                previousUser.EmailConfirmed = false;
                await _unitOfWork.CompleteAsync();
            }
        }

    }
}
