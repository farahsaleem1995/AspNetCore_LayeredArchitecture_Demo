using AutoMapper;
using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Application.Resources.AuthDto;
using LayeredArch.Core.Application.Resources.IdentityDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, 
                                IMapper mapper,
                                IConfiguration config)
        {
            _userService = userService;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userResource)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                var role = await _userService.FindRoleByNameAsync("User");

                var createdUser = await _userService.CreateUserAsync(userResource, role);
                var AccessToken = await GenerateAccessTokenAsync(createdUser);

                return new OkObjectResult(new { AccessToken = AccessToken });
            }

            return new BadRequestObjectResult(new { message = $"Failed to register, {message}!" });
        }

        private async Task<string> GenerateAccessTokenAsync(UserDto user)
        {
            List<string> roles = (List<string>)await _userService.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            DateTime tokenEndTime = DateTime.Now;
            tokenEndTime = tokenEndTime.AddDays(1);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Token:Issuer"],
                audience: _config["Token:Audience"],
                claims: claims,
                expires: tokenEndTime,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
