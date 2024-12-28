using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;
using ProjectManager.Infrastructure.Persistence;
using ProjectManager.Infrastructure.Repositories;
using ProjectManager.Infrastructure.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectManager");
            services.AddDbContext<ProjectManagerDbContext>(option => option.UseSqlServer(connectionString));

            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProjectManagerDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IDataSeeder, UserRolesSeeder>();
            services.AddScoped<IDataSeeder, AdminSeeder>();
            services.AddScoped<IDataSeeder, ProjectRolesSeeder>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectContributorsRepository, ProjectContributorsRepository>();
        }
    }
}
