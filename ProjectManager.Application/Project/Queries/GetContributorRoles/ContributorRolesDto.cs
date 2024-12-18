using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetContributorRoles
{
    public class ContributorRolesDto
    {
        public string UserId { get; set; } = default!;
        public int ProjectId { get; set; } = default!;
        public List<string> AvailableRoles { get; set; } = new();
        public List<string> SelectedRoles { get; set; } = new();
    }
}
