﻿using Microsoft.EntityFrameworkCore;
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
                    .Include(t => t.TaskComments)
                    .ThenInclude(c => c.CreatedBy)
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

        public async Task<TaskComment?> GetCommentById(int id)
            => await _dbContext.TaskComments.FirstOrDefaultAsync(c => c.Id == id);
    }
}