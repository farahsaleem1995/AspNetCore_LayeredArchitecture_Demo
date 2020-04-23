using LayeredArch.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Infra.Data.SeedData
{
    public static class ModelBuilderExtensions
    {
        public static void SeedIdentity(this ModelBuilder modelBuilder)
        {
            var superAdminId = "01b168fe-810b-432d-9010-233ba0b380e9";

            var roleUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            var roleAdminId = "2301d884-221a-4e7d-b509-0113dcc043E1";
            var roleSuperAdminId = "7d9b7113-a8f8-4035-99a7-a20dd400f6a3";

            var roleUser = new DomainRole { Id = roleUserId, Name = "User", NormalizedName = "USER".ToUpper() };
            var roleAdmin = new DomainRole { Id = roleAdminId, Name = "Admin", NormalizedName = "ADMIN".ToUpper() };
            var roleSuperAdmin = new DomainRole { Id = roleSuperAdminId, Name = "SuperAdmin", NormalizedName = "SUPERADMIN".ToUpper() };

            var userName = "admin";
            var email = "admin@admin.com";
            var superAdmin = new DomainUser
            {
                Id = superAdminId,
                FirstName = "Super",
                LastName = "Admin",
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "123a123a",
                PhoneNumberConfirmed = true
            };

            PasswordHasher<DomainUser> ph = new PasswordHasher<DomainUser>();
            superAdmin.PasswordHash = ph.HashPassword(superAdmin, "123a123a");

            modelBuilder.Entity<DomainRole>()
                .HasData(roleUser, roleAdmin, roleSuperAdmin);

            modelBuilder.Entity<DomainUser>()
                .HasData(superAdmin);
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            var superAdminId = "01b168fe-810b-432d-9010-233ba0b380e9";

            var roleUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            var roleAdminId = "2301d884-221a-4e7d-b509-0113dcc043E1";
            var roleSuperAdminId = "7d9b7113-a8f8-4035-99a7-a20dd400f6a3";

            modelBuilder.Entity<DomainUserRole>()
                .HasData(
                    new DomainUserRole() { UserId = superAdminId, RoleId = roleUserId },
                    new DomainUserRole() { UserId = superAdminId, RoleId = roleAdminId },
                    new DomainUserRole() { UserId = superAdminId, RoleId = roleSuperAdminId });
        }
    }
}
