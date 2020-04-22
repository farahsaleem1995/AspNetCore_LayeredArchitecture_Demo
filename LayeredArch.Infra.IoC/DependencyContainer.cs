using AutoMapper;
using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Application.Services;
using LayeredArch.Core.Domain.Interfaces;
using LayeredArch.Core.Domain.Models.Identity;
using LayeredArch.Infra.Data.Context;
using LayeredArch.Infra.Data.Repositories;
using LayeredArch.Infra.Data.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayeredArch.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Data Layer
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<DomainUser, DomainRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<PhoneNumberTokenProvider<DomainUser>>("PhoneNumberTokenProvider");

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                options.SignIn.RequireConfirmedPhoneNumber = true;

                options.Tokens.PasswordResetTokenProvider = "PhoneNumberTokenProvider";
            });

            services.AddAuthorization();

            // Application Layer
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            // Domain Layer
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
