﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Contracts
{
    public interface IDataSeeder
    {
        int Priority { get; }
        Task SeedAsync();
    }
}
