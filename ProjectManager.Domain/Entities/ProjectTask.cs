using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime AssignmentTime { get; set; } = DateTime.Now;
        public DateTime Deadline { get; set; }
        public int ProjectId { get; set; } = default!;
        public Project Project { get; set; } = default!;

    }
}
