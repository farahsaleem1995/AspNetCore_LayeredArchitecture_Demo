using AutoMapper;
using LayeredArch.Api.Resources.Auth;
using LayeredArch.Core.Application.DTO.AuthDto;
using LayeredArch.Core.Application.DTO.IdentityDto;
using LayeredArch.Core.Application.Interfaces;
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
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, 
                                IRefreshTokenService refreshTokenService,
                                IMapper mapper,
                                IConfiguration config)
        {
            _userService = userService;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserResource userResource)
        {
            if (ModelState.IsValid)
            {
                var registerUserDto = _mapper.Map<RegisterUserResource, RegisterUserDto>(userResource);
                var roleDto = await _userService.FindRoleByNameAsync("User");

                var createdUser = await _userService.CreateUserAsync(registerUserDto, roleDto, userResource.Password);

                var code = await _userService.GetPhoneNumberTokenAsync(createdUser.Id, createdUser.PhoneNumber);

                return new OkObjectResult(new { userId = createdUser.Id, Code = code });
            }

            return new BadRequestObjectResult(new { message = "Failed to register" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserResource loginUserResource)
        {
            if (ModelState.IsValid)
            {
                var signedInUser = await _userService.CheckPasswordSignInAsync(loginUserResource.UserName, loginUserResource.Password);

                var accessToken = await GenerateAccessTokenAsync(signedInUser);
                var refreshTokenDto = await _refreshTokenService.GetNew(signedInUser.Id, accessToken);

                return new OkObjectResult(_mapper.Map<RefreshTokenDto, TokenResource>(refreshTokenDto));
            }

            return new BadRequestObjectResult(new { message = "Failed to register" });
        }

        [HttpPost("{userId}/VerifyPhoneNumber")]
        public async Task<ActionResult> VerifyPhoneNumber([FromRoute] string userId,
                                                          [FromQuery] string newPhoneNumber,
                                                          [FromBody] SecurityCodeResource codeResource)
        {
            if (ModelState.IsValid)
            {
                await _userService.ChangePhoneNumberAsync(userId, codeResource.Code, newPhoneNumber: newPhoneNumber);

                return new OkObjectResult(new { message = "Phone number is confirmed successfully." });
            }

            return new BadRequestObjectResult(new { message = "Failed to verify phone number!" });
        }

        [Authorize]
        [HttpGet("ResetPhoneNumber")]
        public async Task<ActionResult> ResetPhoneNumber([FromQuery] string phoneNumber)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var code = await _userService.GetPhoneNumberTokenAsync(loggedInUserId, phoneNumber);

            return new OkObjectResult(new { message = $"You security code is: ({code})." });
        }

        [HttpGet("SendPasswordResetCode")]
        public async Task<IActionResult> SendPasswordResetCode([FromQuery] string userName)
        {
            var code = await _userService.GetPasswordResetTokenAsync(userName);

            // await _emailSender.SendEmailAsync(userEmail, "Reset your password",
            //  $"Please follow this  <a href='{HtmlEncoder.Default.Encode(resetLink)}'>link </a> to reset your password.");

            return new OkObjectResult(new { SecurityCode = code });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string securityCode, [FromBody] ResetPasswordResource resource)
        {
            if (ModelState.IsValid)
            {
                await _userService.ResetPasswordAsync(resource.UserName, securityCode, resource.Password);
                return new OkObjectResult(new { message = "reset password has done successfully." });
            }

            return new BadRequestObjectResult(new { message = "Failed reset Password!" });
        }

        [Authorize]
        [HttpGet("{userId}/SendConfirmationEmail")]
        public async Task<IActionResult> SendConfirmationEmail([FromRoute] string userId)
        {
            var token = await _userService.GetEmailConfirmationTokenAsync(userId);
            var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    values: new EmailConfirmationResource { UserId = userId, Token = token },
                    protocol: Request.Scheme);

            return new OkObjectResult(new { callbackUrl = callbackUrl });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] EmailConfirmationResource emailConfirmationResource)
        {
            await _userService.ConfirmEmailAsync(emailConfirmationResource.UserId, emailConfirmationResource.Token);
            return new OkObjectResult(new { message = "Email has confirmed successfully." });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] CreateTokenResourcecs resource)
        {
            if (ModelState.IsValid)
            {
                var refrehTokenDto = _mapper.Map<CreateTokenResourcecs, RefreshTokenDto>(resource);
                var user = await _refreshTokenService.CheckUser(refrehTokenDto);

                var newAccesToken = await GenerateAccessTokenAsync(user);
                var newRefreshTokenDto = await _refreshTokenService.GetNew(user.Id, newAccesToken);

                return new OkObjectResult(_mapper.Map<RefreshTokenDto, TokenResource>(newRefreshTokenDto));
            }

            return new BadRequestObjectResult(new { message = "Failed to refresh the token!" });
        }

        private async Task<string> GenerateAccessTokenAsync(UserDto user)
        {
            List<string> roles = (List<string>)await _userService.GetRolesAsync(user.Id);

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
