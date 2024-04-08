using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommand : ProjectTaskDto, IRequest
    {
        public string ProjectEncodedName { get; set; } = default!;
    }
}
