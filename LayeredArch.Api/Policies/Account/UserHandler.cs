using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LayeredArch.Api.Policies.Account
{
    public class UserHandler : AuthorizationHandler<UserRequirements>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirements requirement)
        {
            try
            {
                string loggedInUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;
                object routeUserId;
                routeValues.TryGetValue("userId", out routeUserId);
                //throw new Exception(routeUserId.ToString());

                if (loggedInUserId.ToLower() == routeUserId.ToString().ToLower() || _httpContextAccessor.HttpContext.User.IsInRole("Admin"))
                {
                    context.Succeed(requirement);
                }
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
