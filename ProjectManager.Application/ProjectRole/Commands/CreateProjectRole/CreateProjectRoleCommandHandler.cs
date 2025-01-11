using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.ProjectRole.Commands.CreateProjectRole
{
    public class CreateProjectRoleCommandHandler : IRequestHandler<CreateProjectRoleCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateProjectRoleCommandHandler(IProjectRepository projectRepository, IMapper mapper, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<CommandResult> Handle(CreateProjectRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var project = await _projectRepository.GetByEncodedName(request.ProjectEncodedName);
            if (project.CreatedById != user.Id && !user.IsInRole("Admin")) return CommandResult.Failure("You are not entitled to this action");

            if (request.ProjectRoleName.Length == 0 | request.ProjectRoleName.Length > 20) return CommandResult.Failure("The role name must be between 1 and 20 characters. Please provide a valid role name.", 422);

            var projectRolesCount = (await _projectRepository.GetProjectRoles(project.Id)).Count();

            if (projectRolesCount >= 7) return CommandResult.Failure("The project already has the maximum number of 7 roles. No additional roles can be added at this time.");

            var projectRole = _mapper.Map<Domain.Entities.ProjectRole>(new ProjectRoleDto() { Name = request.ProjectRoleName, ProjectId = project.Id });

            await _projectRepository.CreateProjectRole(projectRole);

            return CommandResult.Success("Sucessfully added new project role");
        }
    }
}
