using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetProjectTasks
{
    public class GetProjectTasksQuery : IRequest<IEnumerable<ProjectTaskDto>>
    {
        public string ProjectEncodedName { get; set; } = default!; 
    }
}
