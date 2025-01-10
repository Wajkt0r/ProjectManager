using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = default!;
        public string CreatedByEmail { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsLogTime { get; set; } = default!;
        public TimeSpan? TimeSpent { get; set; }
    }
}
