﻿using System;
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
        public string? EncodedName { get; set; }

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}
