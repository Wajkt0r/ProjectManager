using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Entities
{
    public class ProjectRole
    {
        public int Id { get; set; }
        public int ProjectId { get; set; } = default!;
        public Project Project { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
