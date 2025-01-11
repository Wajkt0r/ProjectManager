using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ProjectManagerDbContext _dbContext;

        public ProjectTaskRepository(ProjectManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit() => await _dbContext.SaveChangesAsync();

        public async Task Create(ProjectTask projectTask)
        {
            _dbContext.Tasks.Add(projectTask);
            await Commit();
        }

        public async Task DeleteTask(ProjectTask projectTask)
        {
            _dbContext.Remove(projectTask);
            await Commit();
        }

        public async Task<IEnumerable<ProjectTask>> GetAllByEncodedName(string projectEncodedName)
        {
            return await _dbContext.Tasks
                .Where(t => t.Project.EncodedName == projectEncodedName)
                .Include(t => t.AssignedUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectTask>> GetUserProjectTasks(string projectEncodedName, string userId) 
            => await _dbContext.Tasks
                    .Where(t => t.Project.EncodedName == projectEncodedName && t.AssignedUserId == userId)
                    .Include(t => t.AssignedUser)
                    .ToListAsync();

        public async Task<IEnumerable<ProjectTask>> GetAllUserTasks(string userId)
            => await _dbContext.Tasks
                    .Where(t => t.AssignedUserId == userId)
                    .Include(t => t.AssignedUser)
                    .ToListAsync();
        
        public async Task<ProjectTask> GetById(int id)
            => await _dbContext.Tasks
                    .Include(t => t.AssignedUser)
                    .FirstAsync(t => t.Id == id);

        public async Task Update(ProjectTask projectTask)
        {
            _dbContext.Tasks.Update(projectTask);
            await Commit();
        }

        public async Task AddComment(TaskComment taskComment)
        {
            _dbContext.TaskComments.Add(taskComment);
            await Commit();
        }

        public async Task DeleteComment(TaskComment taskComment)
        {
            _dbContext.TaskComments.Remove(taskComment);
            await Commit();
        }

        public async Task<IEnumerable<TaskComment>> GetTasksComments(int taskId)
            => await _dbContext.TaskComments.Include(tc => tc.CreatedBy).Where(tc => tc.ProjectTaskId == taskId).ToListAsync();

        public async Task<TaskComment?> GetCommentById(int id)
            => await _dbContext.TaskComments.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<TaskComment>> GetUserComments(IEnumerable<int?> projectTaskIds, string userId)
        {
            var query = _dbContext.TaskComments.Where(t => t.CreatedById == userId);

            if (projectTaskIds.Any())
            {
                query = query.Where(t => projectTaskIds.ToList().Contains((int)t.ProjectTaskId!));
            }

            return await query.ToListAsync();
        }
        public async Task DeleteComments(IEnumerable<TaskComment> userComments)
        {
            _dbContext.TaskComments.RemoveRange(userComments);
            await Commit();
        }

        public async Task LogTime(TimeLog timeLog)
        {
            _dbContext.TimeLogs.Add(timeLog);
            await Commit();
        }

        public async Task<IEnumerable<TimeLog>> GetTasksTimeLogs(int taskId)
            => await _dbContext.TimeLogs.Include(tl => tl.LoggedBy).Where(tl => tl.LoggedInTaskId == taskId).ToListAsync();

        public async Task<TimeLog> GetTimeLogById(int timeLogId)
            => await _dbContext.TimeLogs.FirstOrDefaultAsync(tl => tl.Id == timeLogId);

        public async Task DeleteTimeLog(TimeLog timelog)
        {
            _dbContext.TimeLogs.Remove(timelog);
            await Commit();
        }
    }
}