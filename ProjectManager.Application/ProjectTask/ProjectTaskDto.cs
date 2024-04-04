﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask
{
    public class ProjectTaskDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Deadline { get; set; }
    }
}
