using LayeredArch.Core.Domain.Models.Identity;
using LayeredArch.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Infra.Data.SeedData
{
    public static class SeedDataClass
    {
        public static void Seed(ApplicationDbContext context, RoleManager<DomainRole> roleManager, UserManager<DomainUser> userManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, context);

            context.SaveChanges();
        }

        public static void SeedRoles(RoleManager<DomainRole> roleManager)
        {
            var roles = new List<string>();
            roles.Add("User");
            roles.Add("Admin");
            roles.Add("SuperAdmin");
            foreach (var role in roles)
            {
                var appRole = new DomainRole{ Name = role.ToString() };

                if (!roleManager.RoleExistsAsync(appRole.Name).Result)
                {
                    var roleResult = roleManager.CreateAsync(appRole).Result;
                }
            }
        }

        public static void SeedUsers(UserManager<DomainUser> userManager, ApplicationDbContext context)
        {
            var user = userManager.FindByEmailAsync("admin@admin.com").Result;
            if (user == null)
            {
                user = new DomainUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "admin",
                    LastName = "admin",
                    EmailConfirmed = true,
                    PhoneNumber = "000-000-0000",
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };

                var result = userManager.CreateAsync(user, "123a123a").Result;
                if (result.Succeeded)
                {
                    var roles = new List<string>();
                    roles.Add("User");
                    roles.Add("Admin");
                    roles.Add("SuperAdmin");

                    var roleResult = userManager.AddToRolesAsync(user, roles).Result;
                }
            }
        }
    }
}
