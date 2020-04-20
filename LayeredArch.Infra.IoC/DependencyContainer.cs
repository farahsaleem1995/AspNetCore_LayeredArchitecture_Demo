using AutoMapper;
using LayeredArch.Core.Application.Interfaces;
using LayeredArch.Core.Application.Services;
using LayeredArch.Core.Domain.Models.Identity;
using LayeredArch.Infra.Data.Context;
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
            services.AddIdentity<DomainUser, DomainRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Application Layer
            services.AddScoped<IUserService, UserService>();

            //
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
