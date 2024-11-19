using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Infrastructure.Persistence;

namespace ProjectManager.Infrastructure.Seeders
{
    public class UserRolesSeeder
    {
        private readonly ProjectManagerDbContext _dbContext;

        public UserRolesSeeder(ProjectManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed(RoleManager<IdentityRole> roleManager)
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.UserRoles.Any())
                {
                    var roles = new[] { "Admin", "Manager", "User" };

                    foreach (var role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                            await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }
    }
}
