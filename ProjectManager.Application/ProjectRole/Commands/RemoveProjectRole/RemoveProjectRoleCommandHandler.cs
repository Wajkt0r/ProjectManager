using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.ProjectRole.Commands.RemoveProjectRole
{
    public class RemoveProjectRoleCommandHandler : IRequestHandler<RemoveProjectRoleCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveProjectRoleCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<CommandResult> Handle(RemoveProjectRoleCommand request, CancellationToken cancellationToken)
        {
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

            var projectRole = await _projectRepository.GetProjectRole(projectId, request.ProjectRoleName);

            if (projectRole == null) return CommandResult.Failure("Not found");

            await _projectRepository.RemoveProjectRole(projectRole);

            return CommandResult.Success("Removed project role");
        }
    }
}
