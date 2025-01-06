using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Entities
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime CommentTime { get; set; } = DateTime.UtcNow;
        public string? CreatedById { get; set; } 
        public User? CreatedBy { get; set;}
        public int? ProjectTaskId { get; set; }
        public ProjectTask? Task { get; set; }

    }
}
