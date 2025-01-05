using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Contracts.Services;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.Services
{
    public class TaskManagementService : ITaskManagmentService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public TaskManagementService(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task UnassignTaskForUserInProject(string projectEncodedName, string userId)
        {
            var userProjectTasks = await _projectTaskRepository.GetUserProjectTasks(projectEncodedName, userId);

            await UnAssignTasks(userProjectTasks);
        }

        public async Task UnassignTaskForUser(string userId)
        {
            var allUserTasks = await _projectTaskRepository.GetAllUserTasks(userId);

            await UnAssignTasks(allUserTasks);
        }


        private async Task UnAssignTasks(IEnumerable<ProjectTask> tasks)
        {
            foreach (var task in tasks)
            {
                task.TaskProgressStatus = Domain.Enums.TaskProgressStatus.NotAssigned;
                task.AssignedUserId = null;
                await _projectTaskRepository.Update(task);
            }
        }
    }
}
