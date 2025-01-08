using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectContributors.Queries.GetContributorRoles
{
    public class GetContributorsRolesQueryHandler : IRequestHandler<GetContributorRolesQuery, ContributorRolesDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectContributorsRepository _contributorsRepository;
        private readonly UserManager<User> _userManager;
        public GetContributorsRolesQueryHandler(IProjectRepository projectRepository, IProjectContributorsRepository contributorsRepository, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _contributorsRepository = contributorsRepository;
            _userManager = userManager;
        }
        public async Task<ContributorRolesDto> Handle(GetContributorRolesQuery request, CancellationToken cancellationToken)
        {
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if (user == null) return null;

            var contributorRoles = await _contributorsRepository.GetUserProjectRoles(projectId, user.Id);
            List<Domain.Entities.ProjectRole> availableProjectRoles = await _contributorsRepository.GetAvailableProjectRoles();

            return new ContributorRolesDto
            {
                ProjectId = projectId,
                UserId = user.Id,
                AvailableRoles = availableProjectRoles.Select(pr => pr.Name).ToList(),
                SelectedRoles = contributorRoles
            };

        }
    }
}
