using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectContributors.Queries.GetUserProjectRoles
{
    public class GetUserProjectRolesQueryHandler : IRequestHandler<GetUserProjectRolesQuery, List<string>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectContributorsRepository _contributorsRepository;
        private readonly UserManager<User> _userManager;
        public GetUserProjectRolesQueryHandler(IProjectRepository projectRepository, IProjectContributorsRepository contributorsRepository, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _contributorsRepository = contributorsRepository;
            _userManager = userManager;
        }
        public async Task<List<string>> Handle(GetUserProjectRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserEmail);
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

            var userRoles = await _contributorsRepository.GetUserProjectRoles(projectId, user.Id);

            return userRoles;
        }
    }
}
