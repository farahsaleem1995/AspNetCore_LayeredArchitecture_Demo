using AutoMapper;
using LayeredArch.Api.Resources;
using LayeredArch.Api.Resources.Account;
using LayeredArch.Core.Application.DTO;
using LayeredArch.Core.Application.DTO.IdentityDto;
using LayeredArch.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayeredArch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountsController(IUserService userService,
                                    IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] UserQueryResource queryResource)
        {
            var queryDto = _mapper.Map<UserQueryResource, UserQueryDto>(queryResource);

            var queryResult = await _userService.GetUsersAsync(queryDto);

            var response = _mapper.Map<QueryResultDto<UserDto>, QueryResultResource<UserAdminstrationResource>>(queryResult);

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> FindAccountById([FromRoute] string userId)
        {
            var isAdmin = User.IsInRole("Admin");
            var user = await _userService.FindByIdAsync(userId, isAdmin: isAdmin);
            if (user == null)
            {
                return new NotFoundObjectResult(new { message = "User not found!" });
            }

            UserResource response;
            if (isAdmin)
            {
                response = _mapper.Map<UserDto, UserAdminstrationResource>(user);
            }
            else
            {
                response = _mapper.Map<UserDto, UserResource>(user);
            }

            return new OkObjectResult(response);
        }

        [Authorize]
        [HttpGet("search/{searchKey}")]
        public async Task<IActionResult> SearchAccountByName([FromRoute] string searchKey, [FromQuery] int page, [FromQuery] byte pageSize)
        {
            var queryDto = new UserQueryDto() { Page = page, PageSize = pageSize };
            var isAdmin = User.IsInRole("Admin");
            var users = await _userService.SearchByNameAsync(searchKey, queryDto, isAdmin: isAdmin);

            var response = _mapper.Map<QueryResultDto<UserDto>, QueryResultResource<UserReducedResource>>(users);
            return new OkObjectResult(response);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] string userId, [FromBody] UpdateUserResource updateUserResource)
        {
            if (ModelState.IsValid)
            {
                var isAdmin = User.IsInRole("Admin");
                var updateUserDto = _mapper.Map<UpdateUserResource, UpdateUserDto>(updateUserResource);
                await _userService.UpdateUserAsync(userId, updateUserDto, isAdmin: isAdmin);

                return new OkObjectResult(new { messgae = "User has been updated successfully" });
            }

            return new BadRequestObjectResult(new { message = "Failed to update user account!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] string userId)
        {
            await _userService.DeleteUserAsync(userId);

            return new OkObjectResult(new { message = "User has been disactivated successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{userId}/Activate")]
        public async Task<IActionResult> ActivateAccount([FromRoute] string userId)
        {
            await _userService.ActivateUserAsync(userId);

            return new OkObjectResult(new { message = "User has been activated successfully." });
        }
    }
}
