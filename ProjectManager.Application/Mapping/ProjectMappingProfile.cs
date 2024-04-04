using AutoMapper;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Project;
using ProjectManager.Application.Project.Commands.EditProject;
using ProjectManager.Application.ProjectTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Mapping
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<Domain.Entities.Project, ProjectDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id) || (user.IsInRole("Admin"))))
                .ReverseMap();

            CreateMap<ProjectDto, EditProjectCommand>();

            CreateMap<ProjectTaskDto, Domain.Entities.Project>()
                .ReverseMap();

        }
    }
}
