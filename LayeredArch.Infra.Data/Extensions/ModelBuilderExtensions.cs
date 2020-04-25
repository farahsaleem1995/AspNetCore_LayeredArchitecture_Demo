using LayeredArch.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        private const string SuperAdminId = "01b168fe-810b-432d-9010-233ba0b380e9";
        private const string RoleUserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        private const string RoleAdminId = "2301d884-221a-4e7d-b509-0113dcc043E1";
        private const string RoleSuperAdminId = "7d9b7113-a8f8-4035-99a7-a20dd400f6a3";

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            var roleUser = new DomainRole { Id = RoleUserId, Name = "User", NormalizedName = "USER".ToUpper() };
            var roleAdmin = new DomainRole { Id = RoleAdminId, Name = "Admin", NormalizedName = "ADMIN".ToUpper() };
            var roleSuperAdmin = new DomainRole { Id = RoleSuperAdminId, Name = "SuperAdmin", NormalizedName = "SUPERADMIN".ToUpper() };

            modelBuilder.Entity<DomainRole>()
                .HasData(roleUser, roleAdmin, roleSuperAdmin);
        }

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            
            var userName = "admin";
            var email = "admin@admin.com";
            var superAdmin = new DomainUser
            {
                Id = SuperAdminId,
                FirstName = "Super",
                LastName = "Admin",
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "000-000-0000",
                PhoneNumberConfirmed = true
            };

            PasswordHasher<DomainUser> ph = new PasswordHasher<DomainUser>();
            superAdmin.PasswordHash = ph.HashPassword(superAdmin, "123a123a");

            modelBuilder.Entity<DomainUser>()
                .HasData(superAdmin);
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DomainUserRole>()
                .HasData(
                    new DomainUserRole() { UserId = SuperAdminId, RoleId = RoleUserId },
                    new DomainUserRole() { UserId = SuperAdminId, RoleId = RoleAdminId },
                    new DomainUserRole() { UserId = SuperAdminId, RoleId = RoleSuperAdminId });
        }
    }
}
