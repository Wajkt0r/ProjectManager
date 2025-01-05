using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.Common.Exceptions;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.EditProjectTask
{
    public class EditProjectTaskCommandHandler : IRequestHandler<EditProjectTaskCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectContributorsRepository _projectContributorsRepository;
        private readonly UserManager<User> _userManager;
        public EditProjectTaskCommandHandler(IProjectRepository projectRepository, IProjectTaskRepository projectTaskRepository, IProjectContributorsRepository projectContributorsRepository, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
            _projectContributorsRepository = projectContributorsRepository;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(EditProjectTaskCommand request, CancellationToken cancellationToken)
        { 
            var projectTask = await _projectTaskRepository.GetById(request.Id);

            if (projectTask == null) throw new NotFoundException($"Project task with id: {request.Id} not found");
            if (!request.IsEditable) throw new ForbiddenAccessException("You don't have permissions for this action");

            projectTask.Name = request.Name;
            projectTask.Description = request.Description;
            projectTask.Deadline = request.Deadline;

            if (request.AssignedUserEmail == "Unassigned")
            {
                projectTask.TaskProgressStatus = Domain.Enums.TaskProgressStatus.NotAssigned;
                projectTask.AssignedUserId = null;
                
            } else
            {
                projectTask.TaskProgressStatus = request.TaskProgressStatus == TaskProgressStatus.NotAssigned ? TaskProgressStatus.InProgress : request.TaskProgressStatus;
                projectTask.AssignedUserId = (await _userManager.FindByEmailAsync(request.AssignedUserEmail))?.Id;
            }

            await _projectTaskRepository.Commit();
            return Unit.Value;
        }
    }
}
