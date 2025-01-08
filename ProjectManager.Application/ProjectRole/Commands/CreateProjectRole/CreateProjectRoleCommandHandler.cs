using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.ProjectRole.Commands.CreateProjectRole
{
    public class CreateProjectRoleCommandHandler : IRequestHandler<CreateProjectRoleCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public CreateProjectRoleCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<CommandResult> Handle(CreateProjectRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.ProjectRoleName.Length == 0 | request.ProjectRoleName.Length > 20) return CommandResult.Failure("The role name must be between 1 and 20 characters. Please provide a valid role name.", 422);
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

            var projectRolesCount = (await _projectRepository.GetProjectRoles(projectId)).Count();

            if (projectRolesCount >= 7) return CommandResult.Failure("The project already has the maximum number of 7 roles. No additional roles can be added at this time.");

            var projectRole = _mapper.Map<Domain.Entities.ProjectRole>(new ProjectRoleDto() { Name = request.ProjectRoleName, ProjectId = projectId });

            await _projectRepository.CreateProjectRole(projectRole);

            return CommandResult.Success("Sucessfully added new project role");
        }
    }
}
