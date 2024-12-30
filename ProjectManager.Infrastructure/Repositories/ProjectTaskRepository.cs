using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;
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

        public async Task<ProjectTask> GetById(int id)
            => await _dbContext.Tasks.Include(t => t.AssignedUser).FirstAsync(t => t.Id == id);
    }
}
