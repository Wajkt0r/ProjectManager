using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Infrastructure.Persistence;

namespace ProjectManager.Infrastructure.Seeders
{
    public class ProjectRolesSeeder
    {
        private readonly ProjectManagerDbContext _dbContext;

        public ProjectRolesSeeder(ProjectManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.ProjectRoles.Any())
                {
                    var roles = new[] { "Project Owner", "Frontend Leader", "Frontend", "Backend Leader", "Backend", "Tester Leader", "Tester" };
                    
                    foreach (var role in roles)
                    {
                        _dbContext.ProjectRoles.Add(new Domain.Entities.ProjectRole() { Name = role});
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
