﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LayeredArch.Core.Domain.Models.Identity;
using LayeredArch.Core.Domain.Models.Auth;
using LayeredArch.Infra.Data.Extensions;

namespace LayeredArch.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<
        DomainUser, DomainRole, string,
        IdentityUserClaim<string>, DomainUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Identity Many-to-Many Relations
            builder.Entity<DomainUser>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<DomainRole>()
                .HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Identity
            builder.SeedRoles();
            builder.SeedUsers();
            builder.SeedUserRoles();
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../LayeredArch.Api/appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new ApplicationDbContext(builder.Options);
            }
        }
    }
}
