﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
    {
    }
}
