using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManager.Application.ProjectTask.Queries.GetUserProjectTasks
{
    public class GetUserProjectTasksQuery : IRequest<IEnumerable<ProjectTaskDto>>
    {
        public string ProjectEncodedName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
