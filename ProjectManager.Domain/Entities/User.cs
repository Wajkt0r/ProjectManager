using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Entities
{
    public class User : IdentityUser
    {
        public List<ProjectUser> ProjectUsers { get; set; } = new();
        public List<ProjectUserRole> UserProjectsRoles { get; set; } = new();
    }
}
