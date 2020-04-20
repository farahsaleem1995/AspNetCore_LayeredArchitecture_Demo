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
        }

        public static void SeedRoles(RoleManager<DomainRole> roleManager)
        {
            var roles = new List<string>();
            roles.Add("User");
            roles.Add("Admin");
            foreach (var role in roles)
            {
                var appRole = new DomainRole{ Name = role.ToString() };

                if (!roleManager.RoleExistsAsync(appRole.Name).Result)
                {
                    var roleResult = roleManager.CreateAsync(appRole).Result;
                }
            }
        }
    }
}
