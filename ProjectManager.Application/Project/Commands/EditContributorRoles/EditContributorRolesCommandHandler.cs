using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;

namespace ProjectManager.Application.Project.Commands.EditContributorRoles
{
    public class EditContributorRolesCommandHandler : IRequestHandler<EditContributorRolesCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<User> _userManager;

        public EditContributorRolesCommandHandler(IProjectRepository projectRepository, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
        }

        public async Task<CommandResult> Handle(EditContributorRolesCommand request, CancellationToken cancellationToken)
        {
            var userProjectRoles = await _projectRepository.GetUserProjectRoles(request.ProjectId, request.UserId);

            if (request.SelectedRoles.Except(userProjectRoles).ToList().Count() == 0) return CommandResult.Success("No new roles selected", 304);


            List<ProjectRole> projectRoles = await _projectRepository.GetAvailableProjectRoles();
            var rolesToDelete = userProjectRoles.Except(request.SelectedRoles).ToList();
            var rolesToAdd = request.SelectedRoles.Except(userProjectRoles).ToList();
                 
            if (request.SelectedRoles.Count() > 3)
            {
                return CommandResult.Failure("User can have maximum 3 project roles", 422);
            }

            await _projectRepository.RemoveUserProjectRoles(PrepareRolesList(projectRoles, rolesToDelete, request.ProjectId, request.UserId));
            await _projectRepository.AddUserProjectRoles(PrepareRolesList(projectRoles, rolesToAdd, request.ProjectId, request.UserId));


            return CommandResult.Success("User roles updated succesfully");
        }

        private List<ProjectUserRole> PrepareRolesList(List<ProjectRole> projectRoles, List<string> rolesToPrepare, int projectId, string userId)
        {
            var preparedRoles = new List<ProjectUserRole>();
            foreach (var role in rolesToPrepare)
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
