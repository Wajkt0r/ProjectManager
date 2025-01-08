using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Project;
using ProjectManager.Application.Project.Commands.EditProject;
using ProjectManager.Application.ProjectRole;
using ProjectManager.Application.ProjectTask;
using ProjectManager.Application.Users;
using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Mapping
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile(IUserContext userContext, UserManager<User> userManager)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<Domain.Entities.Project, ProjectDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id) || (user.IsInRole("Admin"))))
                .ReverseMap();

            CreateMap<ProjectDto, EditProjectCommand>();

            CreateMap<ProjectTaskDto, Domain.Entities.ProjectTask>()
                .ForMember(src => src.AssignedUserId, opt => opt.MapFrom(dto => GetUserIdByEmail(dto.AssignedUserEmail, userManager)));

            CreateMap<Domain.Entities.ProjectTask, ProjectTaskDto>()
                .ForMember(dto => dto.AssignedUserEmail, opt => opt.MapFrom(src => src.AssignedUser.Email));

            CreateMap<UserDto, Domain.Entities.User>()
                .ReverseMap();

            CreateMap<ProjectRoleDto, Domain.Entities.ProjectRole>()
                .ReverseMap();

        }

        private static string GetUserIdByEmail(string email, UserManager<User> userManager)
        {
            var user = userManager.FindByEmailAsync(email).Result;
            return user?.Id;
        }
    }
}
