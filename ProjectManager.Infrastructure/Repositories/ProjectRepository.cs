using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Users;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Migrations;
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
            => await _dbContext.Projects.Include(p => p.ProjectTasks).Include(p => p.ProjectContributors).FirstOrDefaultAsync(p => p.EncodedName == encodedName);

        public async Task<Project?> GetByName(string name)
            => await _dbContext.Projects.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

        public async Task DeleteProject(Project project)
        {
            _dbContext.Projects.Remove(project);
            await Commit();
        }

        public async Task<int> GetProjectId(string encodedName)
            => (await _dbContext.Projects.FirstOrDefaultAsync(p => p.EncodedName == encodedName)).Id;


        public async Task<List<Project>> GetAllUserProjects(string userId)
        {
            var projectsId = await _dbContext.ProjectUsers.Where(pu => pu.UserId == userId).Select(pu => pu.ProjectId).ToListAsync();
            var projects = await _dbContext.Projects.Where(p => projectsId.Contains(p.Id)).ToListAsync();

            return projects;
        }

        public async Task DeleteAllUserProjects(string userId)
            => await _dbContext.Projects.Where(p => p.CreatedById == userId).ExecuteDeleteAsync();

        public async Task<ProjectRole> GetProjectRole(int projectId, string roleName)
            => await _dbContext.ProjectRoles.FirstOrDefaultAsync(pr => pr.ProjectId == projectId && pr.Name == roleName);

        public async Task<List<ProjectRole>> GetProjectRoles(int projectId)
            => await _dbContext.ProjectRoles.Where(pr => pr.ProjectId == projectId).ToListAsync();

        public async Task CreateProjectRole(ProjectRole projectRole)
        {
            _dbContext.ProjectRoles.Add(projectRole);
            await Commit();
        }

        public async Task RemoveProjectRole(ProjectRole projectRole)
        {
            _dbContext.ProjectRoles.Remove(projectRole);
            await Commit();
        }

    }
}

