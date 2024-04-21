using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetProjectTaskById
{
    public class GetProjectTaskByIdQuery : IRequest<ProjectTaskDto>
    {
        public int Id { get; set; }

        public GetProjectTaskByIdQuery(int id)
        {
            Id = id;
        }
    }
}
