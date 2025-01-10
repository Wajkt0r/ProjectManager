using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask
{
    public class LogTimeDto
    {
        public string CommitMessage { get; set; } = default!;
        public double TimeSpent { get; set; } = default!;
        public int LoggedInTaskId { get; set; }
        public string LoggedById { get; set; } = default!;
        public DateTime LoggedAt { get; set; } = DateTime.Now;
    }
}
