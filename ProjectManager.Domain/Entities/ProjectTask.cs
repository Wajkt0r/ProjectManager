using ProjectManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        public TaskProgressStatus TaskProgressStatus { get; set; } = default!;
        public string? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
        public DateTime AssignmentTime { get; set; } = DateTime.Now;
        public DateTime Deadline { get; set; } = default!;
        public int ProjectId { get; set; } = default!;
        public Project Project { get; set; } = default!;
        public List<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

    }
}
