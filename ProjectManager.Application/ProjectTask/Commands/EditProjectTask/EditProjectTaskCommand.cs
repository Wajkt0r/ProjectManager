using MediatR;
using ProjectManager.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.EditProjectTask
{
    public class EditProjectTaskCommand : ProjectTaskDto, IRequest
    {
        public IEnumerable<UserDto> ProjectContributors { get; set; } 
        public bool IsEditable { get; set; }
    }
}
