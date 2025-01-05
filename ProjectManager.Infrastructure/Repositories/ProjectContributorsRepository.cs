using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Repositories
{
    public class ProjectContributorsRepository : IProjectContributorsRepository
    {
        private readonly ProjectManagerDbContext _dbContext;
        public ProjectContributorsRepository(ProjectManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task Commit() => await _dbContext.SaveChangesAsync();

        public async Task<IEnumerable<ProjectUser>> GetProjectContributors(int projectId)
            => await _dbContext.ProjectUsers.Where(pu => pu.ProjectId == projectId).ToListAsync();

        public async Task AddContributorToProject(ProjectUser projectUser)
        {
            _dbContext.ProjectUsers.Add(projectUser);
            await Commit();
        }

        public async Task RemoveContributor(ProjectUser projectUser)
        {
            _dbContext.ProjectUsers.Remove(projectUser);
            await Commit();
        }
        public async Task<bool> IsUserContributor(int projectId, string userId)
            => await _dbContext.ProjectUsers.AnyAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);

        public async Task<List<string>> GetUserProjectRoles(int projectId, string userId)
            => await _dbContext.ProjectUserRoles.Where(pur => pur.ProjectId == projectId && pur.UserId == userId)
                .Select(pur => pur.ProjectRole.Name)
                .ToListAsync();

        public async Task<List<ProjectRole>> GetAvailableProjectRoles()
            => await _dbContext.ProjectRoles.ToListAsync();

        public async Task AddUserProjectRoles(List<ProjectUserRole> projectUserRoles)
        {
            _dbContext.ProjectUserRoles.AddRange(projectUserRoles);
            await Commit();
        }


        public async Task RemoveUserProjectRoles(List<ProjectUserRole> projectUserRoles)
        {
            _dbContext.ProjectUserRoles.RemoveRange(projectUserRoles);
            await Commit();
        }
    }
}
