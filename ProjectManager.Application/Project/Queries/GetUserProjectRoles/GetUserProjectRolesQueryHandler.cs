using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;

namespace ProjectManager.Application.Project.Queries.GetUserProjectRoles
{
    public class GetUserProjectRolesQueryHandler : IRequestHandler<GetUserProjectRolesQuery, List<string>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<User> _userManager;
        public GetUserProjectRolesQueryHandler(IProjectRepository projectRepository, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
        }
        public async Task<List<string>> Handle(GetUserProjectRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserEmail);
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

            var userRoles = await _projectRepository.GetUserProjectRoles(projectId, user.Id);

            return userRoles;
        }
    }
}
