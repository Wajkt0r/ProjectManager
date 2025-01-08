using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? CreatedById { get; set; }
        public User? CreatedBy{ get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime FinishDate { get; set; } = default!;
        public string? EncodedName { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; } = new();
        public List<ProjectUser> ProjectContributors { get; set; } = new();
        public List<ProjectRole> ProjectRoles { get; set; } = new();


        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}
