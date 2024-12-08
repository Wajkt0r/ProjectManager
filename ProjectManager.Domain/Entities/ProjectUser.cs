using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Entities
{
    public class ProjectUser
    {
        public string UserId { get; set; } = default!;
        public User User { get; set; } = default!;

        public int ProjectId { get; set; } = default!;
        public Project Project { get; set; } = default!;
    }
}
