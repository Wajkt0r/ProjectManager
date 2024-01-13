using AutoMapper;
using ProjectManager.Application.Project;
using ProjectManager.Application.Project.Commands.EditProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Mapping
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Domain.Entities.Project, ProjectDto>()
                .ReverseMap();

            CreateMap<ProjectDto, EditProjectCommand>();
        }
    }
}
