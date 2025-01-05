using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandHandler : IRequestHandler<CreateProjectTaskCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly UserManager<User> _userManager;

        public CreateProjectTaskCommandHandler(IUserContext userContext, IProjectRepository projectRepository, IProjectTaskRepository projectTaskRepository, UserManager<User> userManager)
        {
            _userContext = userContext;
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByEncodedName(request.ProjectEncodedName);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && (project.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            var assignedUser = request.AssignedUserEmail == null ? null : await _userManager.FindByEmailAsync(request.AssignedUserEmail);

            if (assignedUser != null && request.TaskProgressStatus == Domain.Enums.TaskProgressStatus.NotAssigned)
            {
                request.TaskProgressStatus = Domain.Enums.TaskProgressStatus.InProgress;
            }

            var projectTask = new Domain.Entities.ProjectTask()
            {
                Name = request.Name,
                Description = request.Description,
                TaskProgressStatus = request.TaskProgressStatus,
                AssignedUserId = assignedUser?.Id,
                Deadline = request.Deadline,
                ProjectId = project.Id
            };

            await _projectTaskRepository.Create(projectTask);

            return Unit.Value;
        }
    }
}
