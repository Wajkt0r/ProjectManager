using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project
{
    public class ProjectDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? EncodedName { get; set; }
        public List<Domain.Entities.ProjectTask> ProjectTasks { get; set; } = new();
        public List<Domain.Entities.ProjectUser> ProjectContributors { get; set; } = new();
        public DateTime FinishDate { get; set; } = default!;
        public bool IsEditable { get; set; }
    }
}
