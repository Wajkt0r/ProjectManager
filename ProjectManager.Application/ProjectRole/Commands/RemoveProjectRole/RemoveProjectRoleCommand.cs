using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;

namespace ProjectManager.Application.ProjectRole.Commands.RemoveProjectRole
{
    public class RemoveProjectRoleCommand : IRequest<CommandResult>
    {
        public string ProjectEncodedName { get; set; } = default!;
        public string ProjectRoleName { get; set; } = default!; 
    }
}
