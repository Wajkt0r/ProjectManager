using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;

namespace ProjectManager.Application.Project.Queries.GetContributorRoles
{
    public class GetContributorsRolesQueryHandler : IRequestHandler<GetContributorRolesQuery, ContributorRolesDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<User> _userManager;
        public GetContributorsRolesQueryHandler(IProjectRepository projectRepository, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
        }
        public async Task<ContributorRolesDto> Handle(GetContributorRolesQuery request, CancellationToken cancellationToken)
        {
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if (user == null)
            {
                // wyrzucamy status, ze nie ma uzytkownika
                return null;
            }

            var contributorRoles = await _projectRepository.GetUserProjectRoles(projectId, user.Id);
            List<ProjectRole> availableProjectRoles = await _projectRepository.GetAvailableProjectRoles();

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
