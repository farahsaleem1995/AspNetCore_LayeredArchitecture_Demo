using LayeredArch.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LayeredArch.Api.Policies.Account
{
    public class RoleHandler : AuthorizationHandler<RoleRequirements>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public RoleHandler(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirements requirement)
        {
            try
            {
                string loggedInUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;
                object routeUserId;
                routeValues.TryGetValue("userId", out routeUserId);

                if (_httpContextAccessor.HttpContext.User.IsInRole("SuperAdmin"))
                {
                    context.Succeed(requirement);
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                    var user = userService.FindByIdAsync(routeUserId.ToString()).Result;
                    var userRoles = userService.GetRolesAsync(user.Id).Result;

                    if (userRoles.Contains("SuperAdmin"))
                    {
                        return Task.CompletedTask;
                    }

                    if (_httpContextAccessor.HttpContext.User.IsInRole("SuperAdmin"))
                    {
                        context.Succeed(requirement);
                    }
                    if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
                    {
                        if (!userRoles.Contains("Admin"))
                        {
                            context.Succeed(requirement);
                        }
                    }
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
