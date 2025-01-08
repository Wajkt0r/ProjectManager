using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Contracts.Services;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectContributors.Commands.RemoveContributor
{
    public class RemoveContributorCommandHandler : IRequestHandler<RemoveContributorCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectContributorsRepository _contributorsRepository;
        private readonly ITaskManagmentService _taskManagmentService;
        private readonly ICommentsService _commentsService;
        private readonly UserManager<User> _userManager;

        public RemoveContributorCommandHandler(IProjectRepository projectRepository, IProjectContributorsRepository contributorsRepository, ITaskManagmentService taskManagmentService, ICommentsService commentsService, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _contributorsRepository = contributorsRepository;
            _taskManagmentService = taskManagmentService;
            _commentsService = commentsService;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RemoveContributorCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByEncodedName(request.ProjectEncodedName);
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            var projectUser = new ProjectUser()
            {
                ProjectId = project.Id,
                UserId = user.Id
            };

            // Remove user project roles from db
            var userProjectRoles = await _contributorsRepository.GetUserProjectRoles(project.Id, user.Id);
            var projectRoles = await _contributorsRepository.GetAvailableProjectRoles();
            var rolesToRemove = PrepareRolesList(projectRoles, userProjectRoles, project.Id, user.Id);
            await _contributorsRepository.RemoveUserProjectRoles(rolesToRemove);

            // Remove user's assignment from tasks in the given project and update their status to "Unassigned"
            await _taskManagmentService.UnassignTaskForUserInProject(project.EncodedName, user.Id);

            // Remove user's comments in project
            await _commentsService.DeleteUserComments(project.EncodedName, user.Id);

            await _contributorsRepository.RemoveContributor(projectUser);

            return Unit.Value;
        }

        private List<ProjectUserRole> PrepareRolesList(List<Domain.Entities.ProjectRole> projectRoles, List<string> userProjectRoles, int projectId, string userId)
        {
            var preparedRoles = new List<ProjectUserRole>();
            foreach (var role in userProjectRoles)
            {
                var projectRole = projectRoles.FirstOrDefault(pr => pr.Name == role);
                if (projectRole != null)
                {
                    preparedRoles.Add(new ProjectUserRole()
                    {
                        ProjectId = projectId,
                        UserId = userId,
                        ProjectRoleId = projectRole.Id
                    });
                }
            }
            return preparedRoles;
        }
    }
}
