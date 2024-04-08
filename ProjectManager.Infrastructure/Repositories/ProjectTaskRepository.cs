using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;
using ProjectManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Create(ProjectTask projectTask)
        {
            _dbContext.Tasks.Add(projectTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectTask>> GetAllByEncodedName(string projectEncodedName)
        {
            return await _dbContext.Tasks
                .Where(t => t.Project.EncodedName == projectEncodedName)
                .ToListAsync();
        }
    }
}
