using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.ProjectRole.Commands.RemoveProjectRole
{
    public class RemoveProjectRoleCommandHandler : IRequestHandler<RemoveProjectRoleCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;

        public RemoveProjectRoleCommandHandler(IProjectRepository projectRepository, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
        }

        public async Task<CommandResult> Handle(RemoveProjectRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var project = await _projectRepository.GetByEncodedName(request.ProjectEncodedName);
            if (project.CreatedById != user.Id && !user.IsInRole("Admin")) return CommandResult.Failure("You are not entitled to this action");

            var projectRole = await _projectRepository.GetProjectRole(project.Id, request.ProjectRoleName);

            if (projectRole == null) return CommandResult.Failure("Not found");

            await _projectRepository.RemoveProjectRole(projectRole);

            return CommandResult.Success("Removed project role");
        }
    }
}
