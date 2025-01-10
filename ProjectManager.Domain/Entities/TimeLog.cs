using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Entities
{
    public class TimeLog
    {
        public int Id { get; set; }
        public string CommitMessage { get; set; } = default!;
        public TimeSpan TimeSpent { get; set; } = default!;
        public ProjectTask LoggedInTask { get; set; } = default!;
        public int LoggedInTaskId { get; set; }
        public User LoggedBy { get; set; } = default!;
        public string LoggedById { get; set; } = default!;
        public DateTime LoggedAt { get; set; } = DateTime.Now;
    }
}
