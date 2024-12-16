using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Users;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;
using ProjectManager.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectManagerDbContext _dbContext;

        public ProjectRepository(ProjectManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit() => await _dbContext.SaveChangesAsync();

        public async Task Create(Project project)
        {
            _dbContext.Add(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetAll() 
            => await _dbContext.Projects.ToListAsync();

        public async Task<string?> GetProjectEncodedNameByTaskId(int taskId)
        {
            var projectId = await _dbContext.Tasks
                .Where(t => t.Id == taskId)
                .Select(t => t.ProjectId)
                .FirstOrDefaultAsync();

            var projectEncodedName = await _dbContext.Projects
                .Where(p => p.Id == projectId)
                .Select(p => p.EncodedName)
                .FirstOrDefaultAsync();

            return projectEncodedName;
        }

        public async Task<Project> GetByEncodedName(string encodedName)
            => await _dbContext.Projects.FirstAsync(p => p.EncodedName == encodedName);

        public async Task<Project?> GetByName(string name)
            => await _dbContext.Projects.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

        public async Task DeleteProject(Project project)
        {
            _dbContext.Projects.Remove(project);
            await Commit();
        }

        public async Task<int> GetProjectId(string encodedName)
        {
            var project = await _dbContext.Projects.FirstOrDefaultAsync(p => p.EncodedName == encodedName);
            return project.Id;
        }
        public async Task<IEnumerable<ProjectUser>> GetProjectContributors(int projectId)
            => await _dbContext.ProjectUsers.Where(pu => pu.ProjectId == projectId).ToListAsync();
        public async Task RemoveContributor(ProjectUser projectUser)
        {
            _dbContext.ProjectUsers.Remove(projectUser);
            await Commit();
        }
    }
}
