using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Mapping;
using ProjectManager.Application.Project.Commands.CreateProject;
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
                cfg.AddProfile(new ProjectMappingProfile(userContext));
            }).CreateMapper());

            services.AddMediatR(typeof(CreateProjectCommand));

            services.AddAutoMapper(typeof(ProjectMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>()
                   .AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters();
        }
    }
}
