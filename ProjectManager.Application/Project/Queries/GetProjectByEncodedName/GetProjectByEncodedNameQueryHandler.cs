using AutoMapper;
using MediatR;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Common.Exceptions;
using ProjectManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetProjectByEncodedName
{
    public class GetProjectByEncodedNameQueryHandler : IRequestHandler<GetProjectByEncodedNameQuery, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetProjectByEncodedNameQueryHandler(IProjectRepository projectRepository, IUserContext userContext, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();

            if (user == null) throw new ForbiddenAccessException("You don't have access to this project"); 

            var userProjects = await _projectRepository.GetAllUserProjects(user.Id);
            List<int> userProjectsIds = new List<int>();
            foreach(var projectt in userProjects)
            {
                userProjectsIds.Add(projectt.Id);
            }

            var project = await _projectRepository.GetByEncodedName(request.EncodedName);

            if (project == null) throw new NotFoundException($"Couldn't find project with encoded name {request.EncodedName}");

            if (!userProjectsIds.Contains(project.Id) && !user.IsInRole("Admin")) throw new ForbiddenAccessException("You don't have access to this project");

            var projectDto = _mapper.Map<ProjectDto>(project);
            return projectDto;
        }
    }
}
