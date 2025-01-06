using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Contracts.Services;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public CommentsService(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task DeleteUserComments(string? projectEncodedName, string userId)
        {
            IEnumerable<TaskComment> userComments;
            if (projectEncodedName == null)
            {
                userComments = await _projectTaskRepository.GetUserComments(Enumerable.Empty<int?>(), userId);
            } else
            {
                var taskIds = (await _projectTaskRepository.GetAllByEncodedName(projectEncodedName)).Select(t => (int?)t.Id);
                userComments = await _projectTaskRepository.GetUserComments(taskIds, userId);
            }


            await _projectTaskRepository.DeleteComments(userComments);
        }
    }
}

