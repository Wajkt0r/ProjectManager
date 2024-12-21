using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManager.Application.Project.Queries.GetAllUserProjects
{
    public class GetAllUserProjectsQuery : IRequest<IEnumerable<ProjectDto>>
    {
    }
}
