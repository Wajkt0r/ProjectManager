using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Mapping;
using ProjectManager.Application.Project.Commands.CreateProject;
using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                cfg.AddProfile(new ProjectMappingProfile(userContext, userManager));
            }).CreateMapper());

            services.AddMediatR(typeof(CreateProjectCommand));

            services.AddAutoMapper(typeof(ProjectMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>()
                   .AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters();
        }
    }
}
