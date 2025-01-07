using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Contracts;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.Seeders
{
    public class AdminSeeder : IDataSeeder
    {
        public int Priority => 2;
        private readonly UserManager<User> _userManager;
        public AdminSeeder(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var adminUser = new User
                {
                    UserName = "AdminAccount",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };

                var createAdminAccountResult = await _userManager.CreateAsync(adminUser, "Admin@1");

                if (createAdminAccountResult.Succeeded) await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
