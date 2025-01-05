using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.Project.Queries.GetAllUserProjects
{
    public class GetAllUserProjectsQueryHandler : IRequestHandler<GetAllUserProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetAllUserProjectsQueryHandler(IProjectRepository projectRepository, IUserContext userContext, UserManager<User> userManager, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllUserProjectsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null) return new List<ProjectDto>();

            var projects = currentUser.IsInRole("Admin") ? await _projectRepository.GetAll() : await _projectRepository.GetAllUserProjects(currentUser.Id);

            if (currentUser.IsInRole("Admin"))
            {
                foreach(var project in projects)
                {
                    var creator = await _userManager.FindByIdAsync(project.CreatedById);
                    project.Name = project.Name + $" [{creator.Email}]";
                }
            }

            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }
    }
}
