using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Common;
using ProjectManager.Application.Common.Exceptions;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectContributors.Commands.EditContributorRoles
{
    public class EditContributorRolesCommandHandler : IRequestHandler<EditContributorRolesCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectContributorsRepository _contributorsRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserContext _userContext;

        public EditContributorRolesCommandHandler(IProjectRepository projectRepository, IProjectContributorsRepository contributorsRepository, UserManager<User> userManager, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _contributorsRepository = contributorsRepository;
            _userManager = userManager;
            _userContext = userContext;
        }

        public async Task<CommandResult> Handle(EditContributorRolesCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();

            if (user.Id == null) throw new ForbiddenAccessException("You are not entitled to this action");

            var userProjects = await _projectRepository.GetAllUserProjects(user.Id);

            if (!userProjects.Any(p => p.Id == request.ProjectId) && !user.IsInRole("Admin")) throw new ForbiddenAccessException("You are not entitled to this action");

            var userProjectRoles = await _contributorsRepository.GetUserProjectRoles(request.ProjectId, request.UserId);

            if (request.SelectedRoles.Except(userProjectRoles).ToList().Count() == 0 && userProjectRoles == request.SelectedRoles) return CommandResult.Success("No new roles selected", 304);

            List<Domain.Entities.ProjectRole> projectRoles = await _contributorsRepository.GetAvailableProjectRoles();
            var rolesToDelete = userProjectRoles.Except(request.SelectedRoles).ToList();
            var rolesToAdd = request.SelectedRoles.Except(userProjectRoles).ToList();

            if (request.SelectedRoles.Count() > 3)
            {
                return CommandResult.Failure("User can have maximum 3 project roles", 422);
            }

            await _contributorsRepository.RemoveUserProjectRoles(PrepareRolesList(projectRoles, rolesToDelete, request.ProjectId, request.UserId));
            await _contributorsRepository.AddUserProjectRoles(PrepareRolesList(projectRoles, rolesToAdd, request.ProjectId, request.UserId));


            return CommandResult.Success("User roles updated succesfully");
        }

        private List<ProjectUserRole> PrepareRolesList(List<Domain.Entities.ProjectRole> projectRoles, List<string> rolesToPrepare, int projectId, string userId)
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
